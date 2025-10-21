namespace TABP.WebAPI.Models.HotelGallery;

public class HotelGalleryFilterDto
{
    public int? HotelId { get; set; }
    public int? GalleryId { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}