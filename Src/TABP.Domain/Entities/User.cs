using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public int LocationId { get; set; }

    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required UserRole UserRole { get; set; }
    public required int Age { get; set; }
    public required Gender Gender { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required Location Location { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<UserDiscount> UserDiscounts { get; set; } = new List<UserDiscount>();
}