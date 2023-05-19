using System.Globalization;
using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;

public class SummaryRepository : BaseRepository<Summary>, ISummaryRepository
{
    private readonly DbSet<Summary> _summary;

    public SummaryRepository(DataContext context) : base(context)
    {
        _summary = context.Set<Summary>();
    }
    
    public async Task<List<Summary>> GetByDate(string eodDate)
    {
        var dateValue = DateTime.ParseExact(eodDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return await _summary.Where(p => p.EodDate == dateValue).ToListAsync();
    }
    
}