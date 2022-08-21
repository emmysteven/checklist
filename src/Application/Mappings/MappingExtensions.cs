using AutoMapper;
using AutoMapper.QueryableExtensions;
using Checklist.Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Application.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedResponse<TDestination>> PaginatedResponseAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedResponse<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}