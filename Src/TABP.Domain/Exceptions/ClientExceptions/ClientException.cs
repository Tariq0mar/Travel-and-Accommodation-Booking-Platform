namespace TABP.Domain.Exceptions.ClientExceptions;

public class ClientException : Exception
{
    public int StatusCode { get; }

    public ClientException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}