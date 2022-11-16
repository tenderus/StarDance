using System.ComponentModel.DataAnnotations;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.QueueDtos;

public class QueuePartialUpdateDto
{
    [Range(1, 10)] public int NumberOfOrder { get; set; }

    public virtual ClientPartialUpdateDto ClientPartialUpdateDto { get; set; }

    public virtual LessonPartialUpdateDto LessonPartialUpdateDto { get; set; }
}