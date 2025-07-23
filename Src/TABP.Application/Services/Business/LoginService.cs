using TABP.Domain.BusinessServiceModels.Login;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Interfaces.Repositories.Business;
using TABP.Domain.Interfaces.Services.Business;
using TABP.Domain.Interfaces.Services.CRUD;

namespace TABP.Application.Services.Business;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly IGenerateTokenService _generateTokenService;

    public LoginService(ILoginRepository loginRepository,
        IGenerateTokenService generateTokenService)
    {
        _loginRepository = loginRepository ?? throw new ArgumentException(nameof(loginRepository));
        _generateTokenService = generateTokenService ?? throw new ArgumentException(nameof(generateTokenService));
    }

    public async Task<string> Login(LoginModel loginModel)
    {
        var user = await _loginRepository.LoginAsync(loginModel);

        if (user is null)
        {
            throw new NotFoundException($"no user with these email {loginModel.Email} and password");
        }

        var userCredentials = new UserCredentials
        {
            Id = user.Id,
            Name = user.UserName
        };

        return _generateTokenService.GetToken(userCredentials);
    }
}