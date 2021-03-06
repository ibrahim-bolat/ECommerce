using System.Linq.Expressions;
using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Shared.DataAccess.Abstract;

public interface IGenericRepository<T> where T:class,IEntity,new()
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetAsync(Expression<Func<T,bool>> predicate, params Expression<Func<T,object>>[] includeProperties);
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
    Task<bool> AnyAsync(Expression<Func<T,bool>> predicate);
    Task<int> CountAsync(Expression<Func<T,bool>> predicate);
}