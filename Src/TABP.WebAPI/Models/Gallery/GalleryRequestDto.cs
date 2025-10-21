namespace TABP.WebAPI.Models.Gallery;

public class GalleryRequestDto
{
    public string ImageUrl { get; set; } = string.Empty;
    public string? Caption { get; set; }
}