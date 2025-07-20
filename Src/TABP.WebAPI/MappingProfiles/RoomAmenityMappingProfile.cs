using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomAmenity;

namespace TABP.WebAPI.MappingProfiles;

public class RoomAmenityMappingProfile : Profile
{
    public RoomAmenityMappingProfile()
    {
        CreateMap<RoomAmenityRequestDto, RoomAmenity>();
        CreateMap<RoomAmenityFilterDto, RoomAmenityFilter>();
        CreateMap<RoomAmenity, RoomAmenityResponseDto>();
    }
}