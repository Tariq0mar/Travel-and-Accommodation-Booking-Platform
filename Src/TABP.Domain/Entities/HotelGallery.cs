namespace TABP.Domain.Entities;

public class HotelGallery
{
    public Guid ID { get; set; }
    public Guid GalleryID { get; set; }
    public Guid HotelID { get; set; }

    public required Gallery Gallery { get; set; }
    public required Hotel Hotel { get; set; }
}