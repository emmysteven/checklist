using System.Globalization;
using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;

public class CheckRepository : BaseRepository<Check>, ICheckRepository
{
    private readonly DbSet<Check> _check;

    public CheckRepository(DataContext context) : base(context)
    {
        _check = context.Set<Check>();
    }
    
    public async Task<List<Check>> GetByDate(string eodDate)
    {
        var dateValue = DateTime.ParseExact(eodDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return await _check.Where(p => p.EodDate == dateValue).ToListAsync();
    }
    
    public bool CheckDate(string eodDate)
    {
        var dateValue = DateTime.ParseExact(eodDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return _check.Any(p => p.EodDate == dateValue);
    }
    
}