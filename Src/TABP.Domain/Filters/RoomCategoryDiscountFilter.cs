namespace TABP.Domain.Filters;

public class RoomCategoryDiscountFilter
{
    public Guid? DiscountID { get; set; }
    public Guid? RoomCategoryID { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}