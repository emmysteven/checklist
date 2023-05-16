using System.Globalization;
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
    
    public async Task<List<Item>> GetByDate(string eodDate)
    {
        var dateValue = DateTime.ParseExact(eodDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return await _item.Where(p => p.EodDate == dateValue).ToListAsync();
    }
    
}