namespace TABP.WebAPI.Models.Room;

public class RoomFilterDto
{
    public int? HotelId { get; set; }
    public int? RoomCategoryId { get; set; }
    public bool? IsAvailable { get; set; }

    public string? Number { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}