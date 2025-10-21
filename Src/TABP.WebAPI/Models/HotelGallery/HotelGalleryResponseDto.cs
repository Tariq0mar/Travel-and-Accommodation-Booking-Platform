namespace TABP.WebAPI.Models.HotelGallery;

public class HotelGalleryResponseDto
{
    public int Id { get; set; }
    public int GalleryId { get; set; }
    public int HotelId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public string? Caption { get; set; }
}