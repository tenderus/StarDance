namespace StarDance.Domain;

public class Lesson : Entity
{
    public DateTime DateTime { get; set; }

    public int TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }

    public int RoomId { get; set; }
    public virtual Room Room { get; set; }

    public virtual ICollection<Queue> Queues { get; set; }

    public virtual ICollection<Absence> Absences { get; set; }

    public virtual ICollection<User> Clients { get; set; }
    
    
}