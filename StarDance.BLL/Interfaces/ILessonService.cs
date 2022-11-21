using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;
using StarDance.Common.Helpers;

namespace StarDance.BLL.Interfaces;

public interface ILessonService
{
    Task<LessonReadDto> CreateLessonAsync(LessonUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteLessonAsync(int id, CancellationToken cancellationToken);
    Task<List<LessonReadDto>> GetAllLessonsAsync(CancellationToken cancellationToken);
    Task<LessonReadDto> GetLessonByIdAsync(int id, CancellationToken cancellationToken);
    Task<LessonReadDto> GetLessonByDancetypeAsync(string dancetype, CancellationToken cancellationToken);
    Task<PaginatedResult<LessonReadDto>> GetPagedResult(PagedRequest pagedRequest, CancellationToken cancellationToken);
    Task<List<LessonReadDto>> GetLessonsByClientId(int id, CancellationToken cancellationToken);
    Task<LessonReadDto> UpdateLessonAsync(int id, LessonUpdateDto dto, CancellationToken cancellationToken);
    Task<LessonReadDto> AddClientToLessonAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task DeleteClientFromLessonAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task<bool> IsClientAtLessonAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
}