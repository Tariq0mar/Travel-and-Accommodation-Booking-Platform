using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Amenity;

namespace TABP.WebAPI.MappingProfiles;

public class AmenityMappingProfile : Profile
{
    public AmenityMappingProfile()
    {
        CreateMap<AmenityRequestDto, Amenity>();
        CreateMap<AmenityFilterDto, AmenityFilter>();
        CreateMap<Amenity, AmenityResponseDto>();
    }
}