namespace TABP.Domain.Filters;

public class BookingFilter
{
    public Guid? UserID { get; set; }
    public Guid? RoomID { get; set; }

    public string? Currency { get; set; }

    public decimal? MinTotalPrice { get; set; }
    public decimal? MaxTotalPrice { get; set; }

    public byte? MinAdultsCount { get; set; }
    public byte? MaxAdultsCount { get; set; }

    public byte? MinChildrenCount { get; set; }
    public byte? MaxChildrenCount { get; set; }

    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }

    public DateTime? EndDateFrom { get; set; }
    public DateTime? EndDateTo { get; set; }

    public DateTime? BookingDateFrom { get; set; }
    public DateTime? BookingDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}