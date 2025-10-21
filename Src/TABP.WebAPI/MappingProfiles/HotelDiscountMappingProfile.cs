using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.HotelDiscount;

namespace TABP.WebAPI.MappingProfiles;

public class HotelDiscountMappingProfile : Profile
{
    public HotelDiscountMappingProfile()
    {
        CreateMap<HotelDiscountRequestDto, HotelDiscount>();
        CreateMap<HotelDiscountFilterDto, HotelDiscountFilter>();
        CreateMap<HotelDiscount, HotelDiscountResponseDto>();
    }
}