using System.Net;

namespace StarDance.BLL.Exceptions;

public class DanceClubAPIException : Exception
{
    private HttpStatusCode StatusCode { get; }
    public DanceClubAPIException(string message, HttpStatusCode code) : base(message)
    {
        StatusCode = code;
    }
}