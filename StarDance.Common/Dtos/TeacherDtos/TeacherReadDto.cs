using System.Text.Json.Serialization;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.DanceTypeDtos;

namespace StarDance.Common.Dtos.TeacherDtos;

public class TeacherReadDto
{
    
    public ClientReadDto User { get; set; }
    
    public int DanceTypeId { get; set; }
    
}