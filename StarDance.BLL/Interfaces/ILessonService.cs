using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;
using StarDance.Common.Helpers;

namespace StarDance.BLL.Interfaces;

public interface ILessonService
{
    Task<LessonReadDto> CreateLessonAsync(LessonUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteLessonAsync(int id, CancellationToken cancellationToken);
    Task<List<LessonReadDto>> GetAllLessonsAsync();
    Task<LessonReadDto> GetLessonByIdAsync(int id);
    Task<LessonReadDto> GetLessonByDancetypeAsync(string dancetype);
    Task<PaginatedResult<LessonReadDto>> GetPagedResult(PagedRequest pagedRequest);
    Task<List<LessonsOfClientDto>> GetLessonsByClientId(int id);
    Task<LessonReadDto> UpdateLessonDetailsAsync(int id, LessonPartialUpdateDto dto, CancellationToken cancellationToken);
    Task<LessonReadDto> UpdateLessonAsync(int id, LessonUpdateDto dto, CancellationToken cancellationToken);
    Task<LessonReadDto> AddClientToLessonAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task DeleteClientFromLessonAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task<bool> CheckClientIsAtLessonAsync(LessonClientCreateDto dto);
}