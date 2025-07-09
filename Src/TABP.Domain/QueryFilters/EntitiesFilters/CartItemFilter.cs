namespace TABP.Domain.QueryFilters.EntitiesFilters;

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

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}