using System.Text.Json.Serialization;
using StarDance.Common.Dtos.TeacherDtos;

namespace StarDance.Common.Dtos.DanceTypeDtos;

public class DanceTypeReadDto
{
    public string Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<TeacherReadDto> TeachersReadDtos { get; set; }
}