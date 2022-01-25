using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using Checklist.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    private readonly DbSet<Item> _item;

    public ItemRepository(DataContext context) : base(context)
    {
        _item = context.Set<Item>();
    }
}