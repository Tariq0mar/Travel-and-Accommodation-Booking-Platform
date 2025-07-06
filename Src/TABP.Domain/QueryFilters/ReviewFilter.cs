using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters;

public class ReviewFilter
{
    public Guid? HotelId { get; set; }
    public Guid? UserId { get; set; }


    public StarRating? MinStarRating { get; set; }
    public StarRating? MaxStarRating { get; set; }

    public DateTime? CreationDateFrom { get; set; }
    public DateTime? CreationDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}