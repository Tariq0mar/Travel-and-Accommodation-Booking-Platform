using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.BusinessServiceModels.Login;
using TABP.Domain.Interfaces.Services;
using TABP.WebAPI.Models.Login;

namespace TABP.WebAPI.Controllers;

[ApiController]
[Route("api/Login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IMapper _mapper;

    public LoginController(ILoginService loginService, IMapper mapper)
    {
        _loginService = loginService ?? throw new ArgumentException(nameof(loginService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    public async Task<ActionResult<string>> Login(LoginRequestDto loginRequest)
    {
        var loginModel = _mapper.Map<LoginModel>(loginRequest);
        return await _loginService.Login(loginModel);
    }
}