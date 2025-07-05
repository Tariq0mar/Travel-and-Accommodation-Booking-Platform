namespace TABP.Domain.Entities;

public class UserDiscount
{
    public Guid ID { get; set; }
    public Guid DiscountID { get; set; }
    public Guid UserID { get; set; }

    public required Discount Discount { get; set; }
    public required User User { get; set; }
}