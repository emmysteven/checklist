using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs.Account;
using Checklist.Application.DTOs.Email;
using Checklist.Application.Settings;
using Checklist.Domain.Common;
using Checklist.Domain.Entities;
using Checklist.Domain.Enums;
using Checklist.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace Checklist.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IEmailService _emailService;
    private readonly MailSettings _mailSettings;
    private readonly JWTSettings _jwtSettings;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService(
        DataContext context,
        IMapper mapper,
        JWTSettings jwtSettings,
        IEmailService emailService,
        MailSettings mailSettings)
    {
        _context = context;
        _mapper = mapper;
        _jwtSettings = jwtSettings;
        _emailService = emailService;
        _mailSettings = mailSettings;
    }
        
    private async Task<User> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("Account not found");
        return user;
    }
        
    public async Task<IEnumerable<AuthResponse>> GetAllAsync()
    {
        var users = await _context.Users.FindAsync();
        return _mapper.Map<IEnumerable<AuthResponse>>(users);
    }

    public async Task<Response<AuthResponse>> GetByIdAsync(int id)
    {
        var user = await GetUser(id);
        return _mapper.Map<Response<AuthResponse>>(user);
    }

    public async Task<Response<string?>> RegisterAsync(RegisterRequest request, string origin)
    {
        var isEmailUnique = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (isEmailUnique != null) throw new ApiException($"This Email '{request.Email}' is already taken.");

        var isPhoneNumberUnique = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
        if (isPhoneNumberUnique != null)
            throw new ApiException($"This Phone Number '{request.PhoneNumber}' is already taken.");

        var user = _mapper.Map<User>(request);
        var isFirstAccount = !await _context.Users.AnyAsync();

        user.Role = isFirstAccount ? Roles.Maker : Roles.Checker;
        user.VerificationToken = RandomTokenString();
        user.Created = DateTime.UtcNow;
        user.Password = BC.HashPassword(request.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        // await SendVerificationEmail(user, origin);
        return new Response<string?>(request.FirstName,
            "User registered, please open your email to complete registration");
    }

    public async Task<AuthResponse> AuthenticateAsync(AuthRequest request, string ipAddress)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user == null) throw new ApiException("This account does not exist");

        if (!BC.Verify(request.Password, user.Password))
            throw new ApiException("Your account or password is incorrect");
            
        // Console.WriteLine(user.IsVerified);
        // TODO: add the feature to check if user is verified from frontend
        var jwtToken = GenerateJwt(request);
        var refreshToken = GenerateRefreshToken(ipAddress).Token;
            
        // // save refresh token
        // user.RefreshTokens.Add(refreshToken);
        // await _userRepository.UpdateAsync(user);

        return new AuthResponse
        {
            Token = GenerateJwt(request),
            RefreshToken = GenerateRefreshToken(ipAddress).Token
        };
    }

    public async Task<Response<string?>> VerifyEmailAsync(int id, string token)
    {
        var user = await _context.Users.FindAsync(id);
        Console.WriteLine(user is {IsVerified: true});

        if (user == null) throw new ApiException("Verification failed");
        if (user.IsVerified) return new Response<string?>("This account is already verified.");

        user.Verified = DateTime.UtcNow;
        user.VerificationToken = null!;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        
        return new Response<string?>(user.FirstName, $"Account Confirmed for {user.Email}.");
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        // always return ok response to prevent email enumeration
        if (user == null) return;

        // create reset token that expires after 1 day
        user.ResetToken = RandomTokenString();
        user.ResetTokenExpires = DateTime.UtcNow.AddDays(24);

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        
        SendPasswordResetEmail(user, origin);
    }

    public async Task<Response<string?>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = _context.Users.SingleOrDefault(x => x.Email == request.Email);

        if (user == null) throw new ApiException($"No User is associated with : {request.Email}");

        // update password and remove reset token
        user.Password = BC.HashPassword(request.Password);
        user.PasswordReset = DateTime.UtcNow;
        user.ResetToken = null!;
        user.ResetTokenExpires = null;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return new Response<string?>(user.Email, "Password Reset Done!.");
    }

    private string GenerateJwt(AuthRequest request)
    {
        var user = _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        // string ipAddress = IpHelper.GetIpAddress();
            
        var claims = new[]
        {
            new Claim("Id", user.Result?.Id.ToString() ?? string.Empty),
            new Claim("FirstName", user.Result?.FirstName!),
            new Claim("LastName", user.Result?.LastName!),
            new Claim("Email", user.Result?.Email!),
            new Claim("Role", user.Result?.Role.ToString() ?? string.Empty),
            new Claim("IsVerified", user.Result?.IsVerified.ToString() ?? string.Empty)
            // new Claim("ip", ipAddress)
        };
            
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }
        
    // private async Task<(RefreshToken, GetUser)> GetRefreshToken(string token)
    // {
    //     var user = await _userRepository.FindAsync(u => u.RefreshTokens.Any(t => t.Token == token));
    //     if (user == null) throw new ApiException("Invalid token");
    //     var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
    //     if (!refreshToken.IsActive) throw new ApiException("Invalid token");
    //     return (refreshToken, user);
    // }

    private string RandomTokenString()
    {
        var rng = RandomNumberGenerator.Create();
        var randomBytes = new byte[40];
        rng.GetBytes(randomBytes);
        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    private async Task SendVerificationEmail(User user, string origin)
    {
        string message;
        if (!string.IsNullOrEmpty(origin))
        {
            var verifyUrl = $"{origin}/api/user/verify-email?id={user.Id}&token={user.VerificationToken}";
            message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
        }
        else
        {
            message = $@"<p>Please use the below token to verify your email address with the
                                <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{user.VerificationToken}</code></p>";
        }

        var mail = new EmailRequest
        {
            From = "hello@emmysteven.com",
            To = user.Email,
            Subject = "Sign-up Verification API - Verify Email",
            Body = $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
        };

        await _emailService.SendAsync(mail);
    }

    private void SendPasswordResetEmail(User user, string origin)
    {
        string message;
        if (!string.IsNullOrEmpty(origin))
        {
            var resetUrl = $"{origin}/account/reset-password?token={user.ResetToken}";
            message =
                $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
        }
        else
        {
            message =
                $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{user.ResetToken}</code></p>";
        }

        var emailRequest = new EmailRequest
        {
            From = _mailSettings.EmailFrom,
            To = user.Email,
            Subject = "Sign-up Verification API - Reset Password",
            Body = $@"<h4>Reset Password Email</h4>
                         {message}"
        };

        _emailService.SendAsync(emailRequest);
    }
}