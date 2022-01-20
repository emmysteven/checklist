using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using Checklist.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;

public class TodoRepository : BaseRepository<Todo>, ITodoRepository
{
    private readonly DbSet<Todo> _item;

    public TodoRepository(DataContext context) : base(context)
    {
        _item = context.Set<Todo>();
    }
}