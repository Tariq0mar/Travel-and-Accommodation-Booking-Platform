using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Hotel;

public class HotelRequestDto
{
    public int LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public StarRating StarRating { get; set; }
}