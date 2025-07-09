using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int UserId { get; set; }

    public required StarRating StarRating { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required Hotel Hotel { get; set; }
    public required User User { get; set; }
}