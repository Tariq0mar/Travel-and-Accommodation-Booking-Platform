using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Review;

namespace TABP.WebAPI.MappingProfiles;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<ReviewRequestDto, Review>();
        CreateMap<ReviewFilterDto, ReviewFilter>();
        CreateMap<Review, ReviewResponseDto>();
    }
}