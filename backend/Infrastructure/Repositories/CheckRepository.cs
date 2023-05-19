using System.Globalization;
using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;

public class CheckRepository : BaseRepository<Check>, ICheckRepository
{
    private readonly DbSet<Check> _item;

    public CheckRepository(DataContext context) : base(context)
    {
        _item = context.Set<Check>();
    }
    
    public async Task<List<Check>> GetByDate(string eodDate)
    {
        var dateValue = DateTime.ParseExact(eodDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return await _item.Where(p => p.EodDate == dateValue).ToListAsync();
    }
    
}