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

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken) 
    {
        _context.Set<T>().Remove(entity);
    }
    

    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }
    
    public async Task<List<T>> FindAllWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        var query =  IncludeProperties(includeProperties).Where(predicate).ToListAsync();
        return await query;
    }

    public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        var query =  IncludeProperties(includeProperties);
        var queryToList = await query.ToListAsync();
        return queryToList;
    }
    
    public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        
        return query;
    }
    

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = await IncludeProperties(includeProperties).FirstOrDefaultAsync(x => x.Id == id);
        return query;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<PaginatedResult<T>> GetPagedData(PagedRequest pagedRequest)
    {
        return await _context.Set<T>().CreatePaginatedResult<T>(pagedRequest);
    }
    
    public async Task<PaginatedResult<T>> GetPagedDataWithInclude(PagedRequest pagedRequest, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return await query.CreatePaginatedResult<T>(pagedRequest);
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