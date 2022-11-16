using System.Net;

namespace StarDance.BLL.Exceptions;

public class LessonDoesNotExistException : DanceClubAPIException
{
    public LessonDoesNotExistException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}