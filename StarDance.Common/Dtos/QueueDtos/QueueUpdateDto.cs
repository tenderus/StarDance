using System.ComponentModel.DataAnnotations;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.QueueDtos;

public class QueueUpdateDto
{
    [Range(1, 10)] public int NumberOfOrder { get; set; }

    [Required] public int ClientId { get; set; }

    [Required] public int LessonId { get; set; }
}