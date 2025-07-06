namespace TABP.Domain.QueryFilters;

public class RoomFilter
{
    public Guid? HotelId { get; set; }
    public Guid? CategoryId { get; set; }

    public bool IsAvailable { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}