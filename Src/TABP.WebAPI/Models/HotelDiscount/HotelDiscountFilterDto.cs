namespace TABP.WebAPI.Models.HotelDiscount;

public class HotelDiscountFilterDto
{
    public int? DiscountId { get; set; }
    public int? HotelId { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}