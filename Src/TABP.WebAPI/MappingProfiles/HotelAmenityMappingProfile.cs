using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.HotelAmenity;

namespace TABP.WebAPI.MappingProfiles;

public class HotelAmenityMappingProfile : Profile
{
    public HotelAmenityMappingProfile()
    {
        CreateMap<HotelAmenityRequestDto, HotelAmenity>();
        CreateMap<HotelAmenityFilterDto, HotelAmenityFilter>();
        CreateMap<HotelAmenity, HotelAmenityResponseDto>();
    }
}