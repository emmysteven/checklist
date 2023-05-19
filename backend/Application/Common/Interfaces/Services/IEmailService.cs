using Checklist.Application.DTOs.Email;

namespace Checklist.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendAsync(EmailRequest request);
}