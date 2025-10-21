using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Hotel
{
    public int Id { get; set; }
    public int LocationId { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required StarRating StarRating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required Location Location { get; set; }
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<HotelGallery> HotelGalleries { get; set; } = new List<HotelGallery>();
    public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
    public ICollection<HotelDiscount> HotelDiscounts { get; set; } = new List<HotelDiscount>();
}