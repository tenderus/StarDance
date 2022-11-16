using System.ComponentModel.DataAnnotations;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.AbsenceDtos;

public class AbsenceUpdateDto
{
    [Required] public virtual ClientPartialUpdateDto ClientPartialUpdateDto { get; set; }

    [Required] public virtual LessonPartialUpdateDto LessonPartialUpdateDto { get; set; }
}