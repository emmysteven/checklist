using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.Settings;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;
using Checklist.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace Checklist.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly JWTSettings _jwtSettings;
    private readonly AppSettings _appSettings;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(
        AppSettings appSettings,
        JWTSettings jwtSettings,
        DataContext context,
        IMapper mapper)
    {
        _jwtSettings = jwtSettings;
        _appSettings = appSettings;
        _context = context;
        _mapper = mapper;
    }

    private async Task<User> GetUser(long id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("Account not found");
        return user;
    }

    public async Task<IEnumerable<UserVm>> GetUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserVm>>(users);
    }

    public async Task<Response<UserVm>> GetByIdAsync(long id)
    {
        var user = await GetUser(id);
        return _mapper.Map<Response<UserVm>>(user);
    }

    public async Task<Response<string?>> AddUserAsync(UserDto userDto)
    {
        var isUserNameUnique = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userDto.UserName);
        if (isUserNameUnique != null) throw new ApiException($"'{userDto.UserName}' already exist.");

        var user = _mapper.Map<User>(userDto);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return new Response<string?>(userDto.UserName, "User has been added successfully");
    }

    public AuthVm AuthenticateAsync(AuthDto authDto)
    {
        var url = _appSettings.Url;
        var service = new ExchangeService
        {
            Credentials = new NetworkCredential(authDto.UserName, authDto.Password),
            Url = new Uri(url!)
        };

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        
        var result = service.ConvertIds(new AlternateId[]
        {
            new(IdFormat.HexEntryId, "00", authDto.UserName)
        }, IdFormat.HexEntryId);

        if (result == null) throw new ApiException("This account does not exist");

        var ncCol = service.ResolveName(authDto.UserName, ResolveNameSearchLocation.DirectoryOnly, true);
        var fullName = ncCol == null ? "Unknown" : ncCol[0].Contact.DisplayName;

        return new AuthVm
        {
            FullName = fullName,
            ResponseCode = "00",
            ResponseMessage = "Success",
            Token = GenerateJwt(authDto, fullName)
        };
    }

    private string GenerateJwt(AuthDto authDto, string fullName)
    {
        var user = _context.Users.FirstOrDefaultAsync(x => x.UserName == authDto.UserName);
        var claims = new[]
        {
            new Claim("Id", user.Result?.Id.ToString() ?? string.Empty),
            new Claim("FullName", fullName),
            new Claim("UserName", user.Result?.UserName!),
            new Claim("Role", user.Result?.Role.ToString() ?? string.Empty)
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
}