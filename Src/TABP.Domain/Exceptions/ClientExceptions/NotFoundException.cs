using Microsoft.AspNetCore.Http;

namespace TABP.Domain.Exceptions.ClientExceptions;

public class NotFoundException : ClientException
{
    public NotFoundException(string message = "NotFound")
        : base(message, StatusCodes.Status404NotFound) { }
}