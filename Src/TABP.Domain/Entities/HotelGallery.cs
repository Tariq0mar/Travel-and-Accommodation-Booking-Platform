namespace TABP.Domain.Entities;

public class HotelGallery
{
    public int Id { get; set; }
    public int GalleryId { get; set; }
    public int HotelId { get; set; }

    public required Gallery Gallery { get; set; }
    public required Hotel Hotel { get; set; }
}