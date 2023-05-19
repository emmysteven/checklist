using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces.Services;
using MimeKit;
using Checklist.Application.DTOs.Email;
using Checklist.Application.Settings;
using MailKit.Net.Smtp;

namespace Checklist.Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;
    public ILogger<EmailService> Logger { get; }
    public EmailService(MailSettings mailSettings, ILogger<EmailService> logger)
    {
        _mailSettings = mailSettings;
        Logger = logger;
    }

    public async Task SendAsync(EmailRequest request)
    {
        try
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort);
            await smtp.AuthenticateAsync(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message, ex);
            throw new ApiException(ex.Message);
        }
    }
}