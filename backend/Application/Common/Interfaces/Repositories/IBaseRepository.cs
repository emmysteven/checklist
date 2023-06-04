using System.Linq.Expressions;

namespace Checklist.Application.Common.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetByIdAsync(long id);
        
    Task<List<T>> GetAllAsync();

    Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

    Task<T> FindAsync(Expression<Func<T, bool>> predicate);

    Task<T> CreateAsync(T entity);
    
    Task<IEnumerable<T>> BulkInsertAsync(IEnumerable<T> entity);

    Task<T> UpdateAsync(T entity);

    Task<T> DeleteAsync(T entity);

    Task<int> CountAsync();

    Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate);
}