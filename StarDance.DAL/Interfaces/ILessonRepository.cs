using System.Linq.Expressions;
using StarDance.Domain;

namespace StarDance.DAL.Interfaces;

public interface ILessonRepository
{
    Task<Lesson> GetByDancetypeAsync(string dancetype, params Expression<Func<Lesson, object>>[] includeProperties);
}