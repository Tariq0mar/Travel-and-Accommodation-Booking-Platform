namespace TABP.WebAPI.Models.UserDiscount;

public class UserDiscountFilterDto
{
    public int? DiscountId { get; set; }
    public int? UserId { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}