using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Application.Common.Interfaces;

public interface IDataContext
{
    DbSet<Todo> Todos { get; }

    DbSet<Item> Items { get; }
    
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}