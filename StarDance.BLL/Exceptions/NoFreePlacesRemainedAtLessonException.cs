using System.Net;

namespace StarDance.BLL.Exceptions;

public class NoFreePlacesRemainedAtLessonException : DanceClubAPIException
{
    public NoFreePlacesRemainedAtLessonException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}