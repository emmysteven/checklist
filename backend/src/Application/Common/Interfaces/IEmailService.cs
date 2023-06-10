using Checklist.Application.DTOs;

namespace Checklist.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailDto emailDto);
}