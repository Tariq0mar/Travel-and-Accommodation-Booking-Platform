namespace TABP.Domain.Entities;

public class RoomGallery
{
    public int Id { get; set; }
    public int GalleryId { get; set; }
    public int RoomId { get; set; }

    public required Gallery Gallery { get; set; }
    public required Room Room { get; set; }
}