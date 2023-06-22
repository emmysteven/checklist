using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Common;
using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Persistence;

public class DataContext : DbContext
{
    private readonly IDateService _date;
    private readonly ICurrentUserService _currentUser;
        
    public DataContext(
        DbContextOptions<DataContext> options,
        ICurrentUserService currentUser,
        IDateService date) : base(options)
    {
        _date = date;
        _currentUser = currentUser;
    }

    public virtual DbSet<Todo> Todos => Set<Todo>();
    public virtual DbSet<Check> Checks => Set<Check>();
    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<Summary> Summary => Set<Summary>();
        
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.MakerId = _currentUser.Username ?? string.Empty;
                    entry.Entity.MakerDt = _date.NowUtc;
                    break;
                case EntityState.Modified:
                    entry.Entity.MakerId = _currentUser.Username ?? string.Empty;
                    entry.Entity.MakerDt = _date.NowUtc;
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}