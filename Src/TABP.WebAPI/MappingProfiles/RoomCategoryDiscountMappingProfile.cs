using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomCategoryDiscount;

namespace TABP.WebAPI.MappingProfiles;

public class RoomCategoryDiscountMappingProfile : Profile
{
    public RoomCategoryDiscountMappingProfile()
    {
        CreateMap<RoomCategoryDiscountRequestDto, RoomCategoryDiscount>();
        CreateMap<RoomCategoryDiscountFilterDto, RoomCategoryDiscountFilter>();
        CreateMap<RoomCategoryDiscount, RoomCategoryDiscountResponseDto>();
    }
}