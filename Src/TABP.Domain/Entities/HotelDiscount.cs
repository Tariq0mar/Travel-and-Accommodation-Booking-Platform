namespace TABP.Domain.Entities;

public class HotelDiscount
{
    public int Id { get; set; }
    public int DiscountId { get; set; }
    public int HotelId { get; set; }

    public required Discount Discount { get; set; }
    public required Hotel Hotel { get; set; }
}