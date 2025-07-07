namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomAmenityFilter
{
    public Guid? AmenityId { get; set; }
    public Guid? RoomId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}