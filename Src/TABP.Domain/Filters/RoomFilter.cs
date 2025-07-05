namespace TABP.Domain.Filters;

public class RoomFilter
{
    public Guid? HotelID { get; set; }
    public Guid? CategoryID { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}