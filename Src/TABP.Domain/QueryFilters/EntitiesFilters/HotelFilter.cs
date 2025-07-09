using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class HotelFilter
{
    public Guid? LocationId { get; set; }

    public string? Name { get; set; }
    
    public StarRating? MinStarRating { get; set; }
    public StarRating? MaxStarRating { get; set; }

    public DateTime? CreationDateFrom { get; set; }
    public DateTime? CreationDateTo { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}