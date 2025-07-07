namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomFilter
{
    public Guid? HotelId { get; set; }
    public Guid? CategoryId { get; set; }

    public bool IsAvailable { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}