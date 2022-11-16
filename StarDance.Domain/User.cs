using System.ComponentModel.DataAnnotations;

namespace StarDance.Domain;

public class User : Entity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    [Phone] public string Phone { get; set; }

    public string Email { get; set; }

    public string Login { get; set; }
    
    public string Password {get; set;}
    
    public string Role { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; }

    public virtual ICollection<Queue> Queues { get; set; }

    public virtual ICollection<Absence> Absences { get; set; }
}