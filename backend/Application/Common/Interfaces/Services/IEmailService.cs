using Checklist.Application.DTOs;

namespace Checklist.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendAsync(EmailDto emailDto);
}