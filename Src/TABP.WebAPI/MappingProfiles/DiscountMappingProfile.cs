using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Discount;

namespace TABP.WebAPI.MappingProfiles;

public class DiscountMappingProfile : Profile
{
    public DiscountMappingProfile()
    {
        CreateMap<DiscountRequestDto, Discount>();
        CreateMap<DiscountFilterDto, DiscountFilter>();
        CreateMap<Discount, DiscountResponseDto>();
    }
}