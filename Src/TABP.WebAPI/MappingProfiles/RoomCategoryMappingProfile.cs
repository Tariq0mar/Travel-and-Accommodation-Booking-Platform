using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomCategory;

namespace TABP.WebAPI.MappingProfiles;

public class RoomCategoryMappingProfile : Profile
{
    public RoomCategoryMappingProfile()
    {
        CreateMap<RoomCategoryRequestDto, RoomCategory>();
        CreateMap<RoomCategoryFilterDto, RoomCategoryFilter>();
        CreateMap<RoomCategory, RoomCategoryResponseDto>();
    }
}