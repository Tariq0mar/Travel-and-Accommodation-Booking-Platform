using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Booking;

namespace TABP.WebAPI.MappingProfiles;

public class BookingMappingProfile : Profile
{
    public BookingMappingProfile()
    {
        CreateMap<BookingRequestDto, Booking>();

        CreateMap<BookingFilterDto, BookingFilter>();

        CreateMap<Booking, BookingResponseDto>();
    }
}