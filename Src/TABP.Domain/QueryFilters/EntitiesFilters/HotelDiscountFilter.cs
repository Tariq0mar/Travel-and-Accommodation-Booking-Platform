namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class HotelDiscountFilter
{
    public int? DiscountId { get; set; }
    public int? HotelId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}