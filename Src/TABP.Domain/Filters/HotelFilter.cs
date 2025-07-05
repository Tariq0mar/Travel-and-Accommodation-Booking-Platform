namespace TABP.Domain.Filters;

public class HotelFilter
{
    public Guid? LocationID { get; set; }

    public string? Name { get; set; }
    
    public byte? MinStarRating { get; set; }
    public byte? MaxStarRating { get; set; }

    public DateTime? CreationDateFrom { get; set; }
    public DateTime? CreationDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}