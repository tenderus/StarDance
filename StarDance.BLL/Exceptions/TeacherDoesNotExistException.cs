using System.Net;

namespace StarDance.BLL.Exceptions;

public class TeacherDoesNotExistException : DanceClubAPIException
{
    public TeacherDoesNotExistException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}