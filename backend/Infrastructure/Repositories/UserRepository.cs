using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;
public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(DataContext context) : base(context)
    {
        _users = context.Set<User>();
    }

    public async Task<bool> IsUniqueEmailAsync(string email)
    {
        return await _users.AllAsync(p => p.Email != email);
    }

    public async Task<bool> IsUniquePhoneNumberAsync(string phoneNumber)
    {
        return await _users.AllAsync(p => p.PhoneNumber != phoneNumber);
    }
}