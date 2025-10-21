namespace TABP.WebAPI.Models.Location;

public class LocationFilterDto
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Description { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}