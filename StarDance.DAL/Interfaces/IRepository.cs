using System.Linq.Expressions;
using StarDance.Common.Helpers;
using StarDance.Domain;

namespace StarDance.DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<List<TEntity>> FindAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<PaginatedResult<TEntity>> GetPagedData(PagedRequest pagedRequest, CancellationToken cancellationToken);
    Task<PaginatedResult<TEntity>> GetPagedDataWithInclude(PagedRequest pagedRequest, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);
}