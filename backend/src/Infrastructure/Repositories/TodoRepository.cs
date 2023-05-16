using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using Checklist.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;

public class TodoRepository : BaseRepository<Todo>, ITodoRepository
{
    private readonly DbSet<Todo> _todo;

    public TodoRepository(DataContext context) : base(context)
    {
        _todo = context.Set<Todo>();
    }
}