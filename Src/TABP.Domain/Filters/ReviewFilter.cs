namespace TABP.Domain.Filters;

public class ReviewFilter
{
    public Guid? HotelID { get; set; }
    public Guid? UserID { get; set; }


    public byte? MinRating { get; set; }
    public byte? MaxRating { get; set; }
    
    public DateTime? CreationDateFrom { get; set; }
    public DateTime? CreationDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}