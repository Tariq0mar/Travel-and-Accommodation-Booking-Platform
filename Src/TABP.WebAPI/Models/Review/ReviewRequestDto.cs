using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Review;

public class ReviewRequestDto
{
    public int HotelId { get; set; }
    public int UserId { get; set; }
    public StarRating StarRating { get; set; }
    public string? Description { get; set; }
}