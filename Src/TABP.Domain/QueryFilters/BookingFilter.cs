using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters;

public class BookingFilter
{
    public Guid? UserId { get; set; }
    public Guid? RoomId { get; set; }

    public Currency? Currency { get; set; }

    public decimal? MinTotalPrice { get; set; }
    public decimal? MaxTotalPrice { get; set; }

    public byte? MinAdultsCount { get; set; }
    public byte? MaxAdultsCount { get; set; }

    public byte? MinChildrenCount { get; set; }
    public byte? MaxChildrenCount { get; set; }

    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public DateTime? BookingDateFrom { get; set; }
    public DateTime? BookingDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}