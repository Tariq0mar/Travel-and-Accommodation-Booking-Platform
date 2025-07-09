namespace TABP.Domain.Entities;

public class RoomGallery
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GalleryId { get; set; }
    public Guid RoomId { get; set; }

    public required Gallery Gallery { get; set; }
    public required Room Room { get; set; }
}