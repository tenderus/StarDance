using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.DAL;

public class LessonRepository : Repository<Lesson>, ILessonRepository
{
    private readonly StarDanceContext _context;

    public LessonRepository(StarDanceContext appDbContext) : base(appDbContext)
    {
        _context = appDbContext;
    }

    public async Task<Lesson> GetByDancetypeAsync(string dancetype, CancellationToken cancellationToken, params Expression<Func<Lesson, object>>[] includeProperties)
    {
        var query = await IncludeProperties(includeProperties).FirstOrDefaultAsync(x => x.Teacher.DanceType.Name == dancetype, cancellationToken);
        return query ?? throw new InvalidOperationException();
    }
    
    private IQueryable<Lesson> IncludeProperties(params Expression<Func<Lesson, object>>[] includeProperties)
    {
        IQueryable<Lesson> entities = _context.Set<Lesson>();
        foreach (var includeProperty in includeProperties)
        {
            entities = entities.Include(includeProperty);
        }
        return entities;
    }
}