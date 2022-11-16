using System.Net;

namespace StarDance.BLL.Exceptions;

public class ClientDoesNotExistException : DanceClubAPIException
{
    public ClientDoesNotExistException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}