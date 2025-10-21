using TABP.Domain.Enums;

namespace TABP.Domain.BusinessServiceModels.Login;

public class UserCredentials
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required UserRole Role { get; set; }
}