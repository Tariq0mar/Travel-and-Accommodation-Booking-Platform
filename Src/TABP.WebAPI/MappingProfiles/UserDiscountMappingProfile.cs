using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.UserDiscount;

namespace TABP.WebAPI.MappingProfiles;

public class UserDiscountMappingProfile : Profile
{
    public UserDiscountMappingProfile()
    {
        CreateMap<UserDiscountRequestDto, UserDiscount>();
        CreateMap<UserDiscountFilterDto, UserDiscountFilter>();
        CreateMap<UserDiscount, UserDiscountResponseDto>();
    }
}