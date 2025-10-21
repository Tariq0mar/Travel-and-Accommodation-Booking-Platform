using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<HotelAmenity> HotelAmenities { get; set; }
    public DbSet<HotelDiscount> HotelDiscounts { get; set; }
    public DbSet<HotelGallery> HotelGalleries { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomAmenity> RoomAmenities { get; set; }
    public DbSet<RoomCategory> RoomCategories { get; set; }
    public DbSet<RoomCategoryDiscount> RoomCategoryDiscounts { get; set; }
    public DbSet<RoomGallery> RoomGalleries { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDiscount> UserDiscounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}