using Microsoft.AspNetCore.Http;

namespace TABP.Domain.Exceptions.ClientExceptions;

public class UnauthorizedException : ClientException
{
    public UnauthorizedException(string message = "Unauthorized")
        : base(message, StatusCodes.Status401Unauthorized) { }
}