namespace TABP.WebAPI.Models.RoomGallery;

public class RoomGalleryResponseDto
{
    public int Id { get; set; }
    public int GalleryId { get; set; }
    public int RoomId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public string? Caption { get; set; }
}