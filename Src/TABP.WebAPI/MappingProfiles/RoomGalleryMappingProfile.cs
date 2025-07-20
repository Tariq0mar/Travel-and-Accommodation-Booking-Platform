using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomGallery;

namespace TABP.WebAPI.MappingProfiles;

public class RoomGalleryMappingProfile : Profile
{
    public RoomGalleryMappingProfile()
    {
        CreateMap<RoomGalleryRequestDto, RoomGallery>();
        CreateMap<RoomGalleryFilterDto, RoomGalleryFilter>();
        CreateMap<RoomGallery, RoomGalleryResponseDto>();
    }
}