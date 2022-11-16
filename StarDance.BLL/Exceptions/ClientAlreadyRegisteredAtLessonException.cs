using System.Net;

namespace StarDance.BLL.Exceptions;

public class ClientAlreadyRegisteredAtLessonException : DanceClubAPIException
{
    public ClientAlreadyRegisteredAtLessonException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}