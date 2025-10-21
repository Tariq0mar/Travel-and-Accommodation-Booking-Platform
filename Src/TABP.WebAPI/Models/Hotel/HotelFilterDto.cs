using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Hotel;

public class HotelFilterDto
{
    public int? LocationId { get; set; }
    public string? Name { get; set; }
    public StarRating? StarRating { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}