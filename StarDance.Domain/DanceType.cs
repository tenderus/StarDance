namespace StarDance.Domain;

public class DanceType : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; }
}