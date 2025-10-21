using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomCategoryFilter
{
    public RoomCategoryType? Category { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public byte? MinAdultsCount { get; set; }
    public byte? MaxAdultsCount { get; set; }

    public byte? MinChildrenCount { get; set; }
    public byte? MaxChildrenCount { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}