namespace StarDance.Domain;

public class Queue : Entity
{
    public int NumberOfOrder { get; set; }

    public int ClientId { get; set; }
    public virtual User Client { get; set; }

    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; }
}