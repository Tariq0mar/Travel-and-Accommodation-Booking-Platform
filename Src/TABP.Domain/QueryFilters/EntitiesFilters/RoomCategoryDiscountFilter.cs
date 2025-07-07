namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomCategoryDiscountFilter
{
    public Guid? DiscountId { get; set; }
    public Guid? RoomCategoryId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}