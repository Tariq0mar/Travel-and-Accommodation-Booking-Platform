namespace TABP.WebAPI.Models.RoomAmenity;

public class RoomAmenityFilterDto
{
    public int? RoomId { get; set; }
    public int? AmenityId { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}