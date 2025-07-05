namespace TABP.Domain.Filters;

public class RoomAmenitiesFilter
{
    public Guid? AmenityID { get; set; }
    public Guid? RoomID { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}