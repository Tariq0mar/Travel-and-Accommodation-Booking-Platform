using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Room;

namespace TABP.WebAPI.MappingProfiles;

public class RoomMappingProfile : Profile
{
    public RoomMappingProfile()
    {
        CreateMap<RoomRequestDto, Room>();
        CreateMap<RoomFilterDto, RoomFilter>();
        CreateMap<Room, RoomResponseDto>();
    }
}