namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomFilter
{
    public int? HotelId { get; set; }
    public int? CategoryId { get; set; }

    public bool IsAvailable { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}