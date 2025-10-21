namespace TABP.WebAPI.Models.HotelAmenity;

public class HotelAmenityFilterDto
{
    public int? HotelId { get; set; }
    public int? AmenityId { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}