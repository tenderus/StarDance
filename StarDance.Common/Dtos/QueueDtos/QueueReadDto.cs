using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.QueueDtos;

public class QueueReadDto
{
    public int NumberOfOrder { get; set; }

    public int ClientId { get; set; }

    public int LessonId { get; set; }
}