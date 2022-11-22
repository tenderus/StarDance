using StarDance.Common.Dtos.AbsenceDtos;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.RoomDtos;
using StarDance.Common.Dtos.TeacherDtos;

namespace StarDance.Common.Dtos.LessonDtos;

public class LessonReadDto
{
    public int Id { get; set; }
    
    public DateTime DateTime { get; set; }

    public TeacherReadDto TeacherReadDto { get; set; }

    public RoomReadDto RoomReadDto { get; set; }

    public ICollection<ClientReadDto> Clients { get; set; }

    public int FreePlaces => RoomReadDto.Capacity - Clients.Count;
    
    public ICollection<AbsenceReadDto> Absences { get; set; }
    
}