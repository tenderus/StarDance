using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.AbsenceDtos;

public class AbsenceReadDto
{
    public virtual ClientReadDto ClientReadDto { get; set; }

    public virtual LessonReadDto LessonReadDto { get; set; }
}