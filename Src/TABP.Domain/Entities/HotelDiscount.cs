namespace TABP.Domain.Entities;

public class HotelDiscount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid DiscountId { get; set; }
    public Guid HotelId { get; set; }

    public required Discount Discount { get; set; }
    public required Hotel Hotel { get; set; }
}