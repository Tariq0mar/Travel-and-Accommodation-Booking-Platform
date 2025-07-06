using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters;

public class UserFilter
{
    public Guid? LocationId { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
    
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }

    public Gender? Gender { get; set; }
    public string? PhoneNumber { get; set; }

    public DateTime? CreationDateFrom { get; set; }
    public DateTime? CreationDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}