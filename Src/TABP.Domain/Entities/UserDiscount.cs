namespace TABP.Domain.Entities;

public class UserDiscount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid DiscountId { get; set; }
    public Guid UserId { get; set; }

    public required Discount Discount { get; set; }
    public required User User { get; set; }
}