namespace TABP.Domain.Entities;

public class UserDiscount
{
    public int Id { get; set; }
    public int DiscountId { get; set; }
    public int UserId { get; set; }

    public required Discount Discount { get; set; }
    public required User User { get; set; }
}