using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Discount;

public class DiscountRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DiscountType DiscountType { get; set; }
    public Currency Currency { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}