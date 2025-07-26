namespace TABP.WebAPI.Models.Location;

public class CityVisitorsResponseDto
{
    public string City { get; set; } = default!;
    public List<VisitorCountResponseDto> VisitorsPerYear { get; set; } = new();
}