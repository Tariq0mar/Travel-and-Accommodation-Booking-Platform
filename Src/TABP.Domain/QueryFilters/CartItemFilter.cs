namespace TABP.Domain.QueryFilters;

public class CartItemFilter
{
    public Guid? UserId { get; set; }
    public Guid? RoomId { get; set; }

    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }


    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public bool? BookingConfirmed { get; set; }
    public bool? PaymentCompleted { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}