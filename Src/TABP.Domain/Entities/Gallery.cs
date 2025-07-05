namespace TABP.Domain.Entities;

public class Gallery
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public required string ImageUrl { get; set; }
    public string? Caption { get; set; }
}