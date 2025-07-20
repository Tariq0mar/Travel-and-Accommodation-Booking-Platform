using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.User;

namespace TABP.WebAPI.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRequestDto, User>();
        CreateMap<UserFilterDto, UserFilter>();
        CreateMap<User, UserResponseDto>();
    }
}