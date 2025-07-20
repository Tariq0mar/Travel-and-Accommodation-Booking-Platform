namespace TABP.WebAPI.Models.UserDiscount;

public class UserDiscountResponseDto
{
    public int Id { get; set; }
    public int DiscountId { get; set; }
    public int UserId { get; set; }

    public string DiscountName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}