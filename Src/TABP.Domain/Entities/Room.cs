namespace TABP.Domain.Entities;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid HotelId { get; set; }
    public Guid RoomCategoryId { get; set; }

    public required string Number { get; set; }
    public bool IsAvailable { get; set; } = true;

    public required Hotel Hotel { get; set; }
    public required RoomCategory RoomCategory { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<RoomGallery> RoomGalleries { get; set; } = new List<RoomGallery>();
    public ICollection<RoomAmenity> RoomAmenities { get; set; } = new List<RoomAmenity>();
}