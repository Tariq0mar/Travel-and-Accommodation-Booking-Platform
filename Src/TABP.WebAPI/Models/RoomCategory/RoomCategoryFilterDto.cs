using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.RoomCategory;

public class RoomCategoryFilterDto
{
    public RoomCategoryType? Category { get; set; }
    public Currency? Currency { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public byte? MinAdultsCount { get; set; }
    public byte? MaxAdultsCount { get; set; }

    public byte? MinChildrenCount { get; set; }
    public byte? MaxChildrenCount { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}