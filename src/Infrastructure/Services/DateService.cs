using Checklist.Application.Common.Interfaces;

namespace Checklist.Infrastructure.Services;

public class DateService : IDateService
{
    public DateTime NowUtc => DateTime.UtcNow;
}