using Checklist.Application.DTOs.Email;

namespace Checklist.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailRequest request);
}