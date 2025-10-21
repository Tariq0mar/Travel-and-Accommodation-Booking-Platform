using AutoMapper;
using TABP.Domain.BusinessServiceModels.Login;
using TABP.WebAPI.Models.Login;

namespace TABP.WebAPI.MappingProfiles;

public class LoginMappingProfile : Profile
{
    public LoginMappingProfile()
    {
        CreateMap<LoginRequestDto, LoginModel>();
    }
}