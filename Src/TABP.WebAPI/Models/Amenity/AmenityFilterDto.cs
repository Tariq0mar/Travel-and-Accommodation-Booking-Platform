namespace TABP.WebAPI.Models.Amenity;

public class AmenityFilterDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}