using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Hotel;

namespace TABP.WebAPI.MappingProfiles;

public class HotelMappingProfile : Profile
{
    public HotelMappingProfile()
    {
        CreateMap<HotelRequestDto, Hotel>();
        CreateMap<HotelFilterDto, HotelFilter>();
        CreateMap<Hotel, HotelResponseDto>();
    }
}