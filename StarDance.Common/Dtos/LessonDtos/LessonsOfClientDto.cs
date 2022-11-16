using StarDance.Common.Dtos.DanceTypeDtos;
using StarDance.Common.Dtos.RoomDtos;
using StarDance.Common.Dtos.TeacherDtos;

namespace StarDance.Common.Dtos.LessonDtos;

public class LessonsOfClientDto
{
    public int Id { get; set; }
    
    public DateTime DateTime { get; set; }

    public TeacherReadDto TeacherReadDto { get; set; }

    public RoomReadDto RoomReadDto { get; set; }
    
    public string DanceType { get; set; }
    
    public int FreePlaces { get; set; }
    

}