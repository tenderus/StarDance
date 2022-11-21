using System.ComponentModel.DataAnnotations;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.AbsenceDtos;

public class AbsenceUpdateDto
{
    [Required] public int ClientId { get; set; }

    [Required] public int LessonId { get; set; }
}