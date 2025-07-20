using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Payment;

namespace TABP.WebAPI.MappingProfiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<PaymentRequestDto, Payment>();
        CreateMap<PaymentFilterDto, PaymentFilter>();
        CreateMap<Payment, PaymentResponseDto>();
    }
}