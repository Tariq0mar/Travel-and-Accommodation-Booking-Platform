using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Hotel;

public class HotelResponseDto
{
    public int Id { get; set; }
    public int LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public StarRating StarRating { get; set; }
    public DateTime CreatedAt { get; set; }
}