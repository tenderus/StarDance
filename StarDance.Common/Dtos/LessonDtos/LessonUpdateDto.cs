using System.ComponentModel.DataAnnotations;

namespace StarDance.Common.Dtos.LessonDtos;

public class LessonUpdateDto
{
    [Required] public DateTime DateTime { get; set; }

    [Required] public int TeacherPartialUpdateDtoId { get; set; }

    [Required] public int RoomPartialUpdateDtoId { get; set; }
}