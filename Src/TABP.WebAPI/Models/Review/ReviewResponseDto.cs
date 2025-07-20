using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Review;

public class ReviewResponseDto
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int UserId { get; set; }
    public StarRating StarRating { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}