namespace TABP.WebAPI.Models.RoomGallery;

public class RoomGalleryFilterDto
{
    public int? RoomId { get; set; }
    public int? GalleryId { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}