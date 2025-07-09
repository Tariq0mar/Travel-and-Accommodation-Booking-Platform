namespace TABP.Domain.Exceptions.ServerExceptions;

public class ServerException : Exception
{
    public int StatusCode { get; }

    public ServerException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}