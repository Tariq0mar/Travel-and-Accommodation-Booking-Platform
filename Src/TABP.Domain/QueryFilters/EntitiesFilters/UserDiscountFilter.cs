namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class UserDiscountFilter
{
    public int? DiscountId { get; set; }
    public int? UserId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}