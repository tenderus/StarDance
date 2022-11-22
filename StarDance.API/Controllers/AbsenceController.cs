using Microsoft.AspNetCore.Mvc;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.API.Controllers;

[Route("api/absences")]
[ApiController]
public class AbsenceController : ControllerBase
{
    private readonly IAbsenceService _absenceService;

    public AbsenceController(IAbsenceService absenceService)
    {
        _absenceService = absenceService;
    }

    [HttpPost]
    public async Task AddAbsence(LessonClientCreateDto dto, CancellationToken cancellationToken)
    {
        await _absenceService.AddInAbsencesAsync(dto, cancellationToken);
    }
    
    [HttpDelete]
    public async Task DeleteAbsence(LessonClientCreateDto dto, CancellationToken cancellationToken)
    {
        await _absenceService.DeleteFromAbsencesAsync(dto, cancellationToken);
    }
}