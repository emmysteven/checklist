using System.Linq.Expressions;
using Checklist.Application.Common.Interfaces;
using Checklist.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DataContext _context;
    public BaseRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return (await _context.Set<T>().FindAsync(id))!;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();    
    }

    public async Task<IEnumerable<T>> BulkInsertAsync(IEnumerable<T> entity)
    {
        await _context.Set<T>().BulkInsertAsync(entity.ToList());
        await _context.BulkSaveChangesAsync();
        return entity.ToList();
    }

    public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
    {
        return await _context
            .Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return (await _context.Set<T>().FirstOrDefaultAsync(predicate))!;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Update(entity);
        // _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }

    public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().CountAsync(predicate);
    }
}