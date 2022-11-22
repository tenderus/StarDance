using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.DanceTypeDtos;

namespace StarDance.Common.Dtos.TeacherDtos;

public class TeacherReadDto
{
    public ClientReadDto User { get; set; }
    
    public DanceTypeReadDto DanceType { get; set; }
    
    public int Age { get; set; }
    
    public int YearsOfExperience { get; set; }
    
    public string Description { get; set; }
}