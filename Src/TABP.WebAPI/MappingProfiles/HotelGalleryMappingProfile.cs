using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.HotelGallery;

namespace TABP.WebAPI.MappingProfiles;

public class HotelGalleryMappingProfile : Profile
{
    public HotelGalleryMappingProfile()
    {
        CreateMap<HotelGalleryRequestDto, HotelGallery>();
        CreateMap<HotelGalleryFilterDto, HotelGalleryFilter>();
        CreateMap<HotelGallery, HotelGalleryResponseDto>();
    }
}