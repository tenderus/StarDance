using System.Net;

namespace StarDance.BLL.Exceptions;

public class AbsenceDoesNotExist: DanceClubAPIException
{
    public AbsenceDoesNotExist(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}