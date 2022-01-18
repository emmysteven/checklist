using Checklist.Application.Settings;

namespace Checklist.Application.Common.Interfaces;

public interface ICacheInvalidatorPostProcessor
{
    InvalidateCacheForQueries QueriesList { get; set; }
}