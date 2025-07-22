using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Gallery;

namespace TABP.WebAPI.MappingProfiles;

public class GalleryMappingProfile : Profile
{
    public GalleryMappingProfile()
    {
        CreateMap<GalleryRequestDto, Gallery>();
        CreateMap<GalleryFilterDto, GalleryFilter>();
        CreateMap<Gallery, GalleryResponseDto>();
    }
}