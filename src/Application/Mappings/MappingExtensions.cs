using AutoMapper;
using AutoMapper.QueryableExtensions;
using Checklist.Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Application.Mappings;

public static class MappingExtensions
{
    public static Task<PaginateResponse<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginateResponse<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}