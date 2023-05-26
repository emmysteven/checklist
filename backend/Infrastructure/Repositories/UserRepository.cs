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
}