namespace TABP.Domain.Filters;

public class CartFilter
{
    public Guid? UserID { get; set; }
    public Guid? RoomID { get; set; }

    public DateTime AddedAtFrom { get; set; }
    public DateTime AddedAtTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}