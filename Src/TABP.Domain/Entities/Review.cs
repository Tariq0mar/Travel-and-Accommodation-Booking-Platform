namespace TABP.Domain.Entities;

public class Review
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid HotelID { get; set; }
    public Guid UserID { get; set; }

    public required byte Rating { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public required Hotel Hotel { get; set; }
    public required User User { get; set; }
}