using Microsoft.AspNetCore.Http;

namespace TABP.Domain.Exceptions.ClientExceptions;

public class BadRequestException : ClientException
{
    public BadRequestException(string message = "BadRequest")
        : base(message, StatusCodes.Status400BadRequest) { }
}