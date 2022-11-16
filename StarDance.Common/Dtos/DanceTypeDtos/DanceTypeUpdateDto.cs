using System.ComponentModel.DataAnnotations;

namespace StarDance.Common.Dtos.DanceTypeDtos;

public class DanceTypeUpdateDto
{
    [Required]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Dancetype name is too short or too long")]
    public string Name { get; set; }
}