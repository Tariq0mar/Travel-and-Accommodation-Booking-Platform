namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomAmenityFilter
{
    public int? AmenityId { get; set; }
    public int? RoomId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}