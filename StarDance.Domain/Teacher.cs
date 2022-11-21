namespace StarDance.Domain;

public class Teacher : Entity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int DanceTypeId { get; set; }
    public virtual DanceType DanceType { get; set; }
    
    public int Age { get; set; }
    
    public int YearsOfExperience { get; set; }
    
    public string Description { get; set; }
    
    public virtual ICollection<Lesson> Lessons { get; set; }
}