namespace StarDance.Domain;

public class Room : Entity
{
    public int Number { get; set; }

    public int Capacity { get; set; }

    public ICollection<Lesson> Lessons { get; set; }
}