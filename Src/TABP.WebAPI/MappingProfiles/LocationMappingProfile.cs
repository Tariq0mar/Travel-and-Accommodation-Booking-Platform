using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Location;

namespace TABP.WebAPI.MappingProfiles;

public class LocationMappingProfile : Profile
{
    public LocationMappingProfile()
    {
        CreateMap<LocationRequestDto, Location>();
        CreateMap<LocationFilterDto, LocationFilter>();
        CreateMap<Location, LocationResponseDto>();
    }
}