using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;

namespace Checklist.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task<Response<AuthVm>> GetByIdAsync(int id);
    Task<IEnumerable<AuthVm>> GetUsersAsync();
    Task<Response<string?>> AddUserAsync(UserDto dto);
    AuthVm AuthenticateAsync(AuthDto authDto);
}