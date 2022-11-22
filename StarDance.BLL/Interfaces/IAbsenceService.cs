using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.BLL.Interfaces;

public interface IAbsenceService
{ 
    Task AddInAbsencesAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task DeleteFromAbsencesAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
}