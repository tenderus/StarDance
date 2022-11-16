using System.Linq.Expressions;
using StarDance.Common.Helpers;
using StarDance.Domain;

namespace StarDance.DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
    Task<List<TEntity>> FindAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<PaginatedResult<TEntity>> GetPagedData(PagedRequest pagedRequest);
    Task<PaginatedResult<TEntity>> GetPagedDataWithInclude(PagedRequest pagedRequest, params Expression<Func<TEntity, object>>[] includeProperties);
}