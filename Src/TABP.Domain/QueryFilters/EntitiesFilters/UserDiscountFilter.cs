namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class UserDiscountFilter
{
    public Guid? DiscountId { get; set; }
    public Guid? UserId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}