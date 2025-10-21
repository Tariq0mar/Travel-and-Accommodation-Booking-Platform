namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class HotelAmenityFilter
{
    public int? AmenityId { get; set; }
    public int? HotelId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}