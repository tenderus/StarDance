using System.ComponentModel.DataAnnotations;

namespace StarDance.Common.Dtos.LessonDtos;

public class LessonClientCreateDto
{
    [Required]
    public int ClientId { get; set; }
    
    [Required]
    public int LessonId { get; set; }
}