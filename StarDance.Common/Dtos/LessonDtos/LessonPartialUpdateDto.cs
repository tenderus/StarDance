namespace StarDance.Common.Dtos.LessonDtos;

public class LessonPartialUpdateDto
{
    public DateTime? DateTime { get; set; }

    public int? TeacherId { get; set; }

    public int? RoomId { get; set; }
}