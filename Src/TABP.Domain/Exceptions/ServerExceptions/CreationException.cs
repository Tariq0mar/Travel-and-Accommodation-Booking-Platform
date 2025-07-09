using Microsoft.AspNetCore.Http;

namespace TABP.Domain.Exceptions.ServerExceptions;

public class CreationException : ServerException
{
    public CreationException(string message = "Failed to create")
        : base(message, StatusCodes.Status500InternalServerError)
    {
    }
}