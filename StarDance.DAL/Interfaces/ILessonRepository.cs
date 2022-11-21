using System.Linq.Expressions;
using StarDance.Domain;

namespace StarDance.DAL.Interfaces;

public interface ILessonRepository : IRepository<Lesson>
{
    Task<Lesson> GetByDancetypeAsync(string dancetype,CancellationToken cancellationToken, params Expression<Func<Lesson, object>>[] includeProperties);
}