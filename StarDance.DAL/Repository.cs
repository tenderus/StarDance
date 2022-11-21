using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StarDance.Common.Helpers;
using StarDance.DAL.Extensions;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.DAL;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly StarDanceContext _context;

    public Repository(StarDanceContext appDbContext)
    {
        _context = appDbContext;
    }
    
     public async Task SaveChangesAsync(CancellationToken cancellationToken)
     {
         await _context.SaveChangesAsync(cancellationToken);
     }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity) 
    {
        _context.Set<T>().Remove(entity);
    }
    

    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }
    
    public async Task<List<T>> FindAllWithIncludeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeProperties)
    {
        var query =  IncludeProperties(includeProperties).Where(predicate).ToListAsync(cancellationToken);
        return await query;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeProperties)
    {
        var query =  IncludeProperties(includeProperties);
        var queryToList = await query.ToListAsync(cancellationToken);
        return queryToList;
    }
    
    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = await IncludeProperties(includeProperties).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return query;
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<PaginatedResult<T>> GetPagedData(PagedRequest pagedRequest, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().CreatePaginatedResult<T>(pagedRequest, cancellationToken);
    }
    
    public async Task<PaginatedResult<T>> GetPagedDataWithInclude(PagedRequest pagedRequest, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return await query.CreatePaginatedResult<T>(pagedRequest, cancellationToken);
    }

    private IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> entities = _context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
            entities = entities.Include(includeProperty);
        }
        return entities;
    }
}