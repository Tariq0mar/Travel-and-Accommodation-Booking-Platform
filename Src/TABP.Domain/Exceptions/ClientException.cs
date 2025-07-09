namespace TABP.Domain.Exceptions;

public class ClientException : Exception
{
    public int StatusCode { get; }

    protected ClientException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}