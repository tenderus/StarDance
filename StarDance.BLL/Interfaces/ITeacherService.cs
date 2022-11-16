using StarDance.Common.Dtos.TeacherDtos;

namespace StarDance.BLL.Interfaces;

public interface ITeacherService
{
    Task<List<TeacherReadDto>> GetAllTeachersAsync();
    Task<TeacherReadDto> GetTeacherByIdAsync(int id);
    Task<TeacherReadDto> GetTeacherByDancetypeAsync(string dancetype);
    Task<TeacherReadDto> AddTeacherAsync(TeacherUpdateDto dto, CancellationToken cancellationToken);
    Task<TeacherReadDto> UpdateTeacherDetailsAsync(int id, TeacherPartialUpdateDto dto, CancellationToken cancellationToken);
    Task<TeacherReadDto> UpdateTeacherAsync(int id, TeacherUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteTeacherAsync(int id, CancellationToken cancellationToken);
}