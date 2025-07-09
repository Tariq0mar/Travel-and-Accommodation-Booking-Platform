namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomCategoryDiscountFilter
{
    public int? DiscountId { get; set; }
    public int? RoomCategoryId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}