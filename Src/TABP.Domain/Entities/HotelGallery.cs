namespace TABP.Domain.Entities;

public class HotelGallery
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GalleryId { get; set; }
    public Guid HotelId { get; set; }

    public required Gallery Gallery { get; set; }
    public required Hotel Hotel { get; set; }
}