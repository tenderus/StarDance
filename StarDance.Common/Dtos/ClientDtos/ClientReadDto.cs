using System.ComponentModel.DataAnnotations;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.Common.Dtos.ClientDtos;

public class ClientReadDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Surname { get; set; }

    [Phone] public string Phone { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Role { get; set; }
    
    public virtual ICollection<LessonsOfClientDto> LessonsOfClientDtos { get; set; }
}