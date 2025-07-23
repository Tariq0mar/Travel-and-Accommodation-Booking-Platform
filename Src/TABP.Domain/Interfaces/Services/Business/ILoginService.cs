using TABP.Domain.BusinessServiceModels.Login;
using TABP.Domain.Entities;

namespace TABP.Domain.Interfaces.Services.Business;

public interface ILoginService
{
    public Task<string> Login(LoginModel loginModel);
}