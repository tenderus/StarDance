namespace StarDance.Domain;

public class Absence : Entity
{
    public int ClientId { get; set; }
    public virtual User Client { get; set; }

    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; }
}