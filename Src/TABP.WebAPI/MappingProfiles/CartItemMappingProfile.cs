using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.CartItem;

namespace TABP.WebAPI.MappingProfiles;

public class CartItemMappingProfile : Profile
{
    public CartItemMappingProfile()
    {
        CreateMap<CartItemRequestDto, CartItem>();
        CreateMap<CartItemFilterDto, CartItemFilter>();
        CreateMap<CartItem, CartItemResponseDto>();
    }
}