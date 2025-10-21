using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Review;

public class ReviewFilterDto
{
    public int? HotelId { get; set; }
    public int? UserId { get; set; }
    public StarRating? StarRating { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}