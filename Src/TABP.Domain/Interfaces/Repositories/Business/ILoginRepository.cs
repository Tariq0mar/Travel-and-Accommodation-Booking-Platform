using TABP.Domain.BusinessServiceModels.Login;
using TABP.Domain.Entities;

namespace TABP.Domain.Interfaces.Repositories.Business;

public interface ILoginRepository
{
    Task<User?> LoginAsync(LoginModel loginModel);
}