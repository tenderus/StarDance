using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.AbsenceDtos;

public class AbsencePartialUpdateDto
{
    public virtual ClientPartialUpdateDto ClientPartialUpdateDto { get; set; }

    public virtual LessonPartialUpdateDto LessonPartialUpdateDto { get; set; }
}