namespace TABP.Domain.Entities;

public class HotelDiscount
{
    public Guid ID { get; set; }
    public Guid DiscountID { get; set; }
    public Guid HotelID { get; set; }

    public required Discount Discount { get; set; }
    public required Hotel Hotel { get; set; }
}