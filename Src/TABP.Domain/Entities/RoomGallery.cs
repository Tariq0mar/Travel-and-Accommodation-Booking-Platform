namespace TABP.Domain.Entities;

public class RoomGallery
{
    public Guid ID { get; set; }
    public Guid GalleryID { get; set; }
    public Guid RoomID { get; set; }

    public required Gallery Gallery { get; set; }
    public required Room Room { get; set; }
}