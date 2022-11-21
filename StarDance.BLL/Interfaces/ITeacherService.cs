using StarDance.Common.Dtos.TeacherDtos;
using StarDance.Common.Helpers;

namespace StarDance.BLL.Interfaces;

public interface ITeacherService
{
    Task<List<TeacherReadDto>> GetAllTeachersAsync(CancellationToken cancellationToken);
    Task<TeacherReadDto> GetTeacherByIdAsync(int id, CancellationToken cancellationToken);
    Task<TeacherReadDto> GetTeacherByDancetypeAsync(string dancetype, CancellationToken cancellationToken);
    Task<PaginatedResult<TeacherReadDto>> GetPagedResult(PagedRequest pagedRequest, CancellationToken cancellationToken);
    Task<TeacherReadDto> AddTeacherAsync(TeacherUpdateDto dto, CancellationToken cancellationToken);
    Task<TeacherReadDto> UpdateTeacherAsync(int id, TeacherUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteTeacherAsync(int id, CancellationToken cancellationToken);
}