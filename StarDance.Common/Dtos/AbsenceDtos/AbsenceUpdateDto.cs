using System.ComponentModel.DataAnnotations;

namespace StarDance.Common.Dtos.AbsenceDtos;

public class AbsenceUpdateDto
{
    [Required] public int ClientId { get; set; }

    [Required] public int LessonId { get; set; }
}