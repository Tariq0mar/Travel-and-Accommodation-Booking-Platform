using TABP.Domain.BusinessServiceModels.Login;

namespace TABP.Domain.Interfaces.Services.Business;

public interface IGenerateTokenService
{
    public string GetToken(UserCredentials user);
}