using StarDance.Common.Dtos.AbsenceDtos;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;

namespace StarDance.Common.Dtos.ClientDtos;

public class ClientLessonsQueuesAbsencesPartialUpdateDto
{
    public virtual ICollection<LessonPartialUpdateDto> LessonsPartialUpdateDtos { get; set; }

    public virtual ICollection<QueuePartialUpdateDto> QueuesPartialUpdateDtos { get; set; }

    public virtual ICollection<AbsencePartialUpdateDto> AbsencesPartialUpdateDtos { get; set; }
}