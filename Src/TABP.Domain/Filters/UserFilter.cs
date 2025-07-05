namespace TABP.Domain.Filters;

public class UserFilter
{
    public Guid? LocationID { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? HashedPassword { get; set; }
    public byte? Role { get; set; }
    
    public byte? MinAge { get; set; }
    public byte? MaxAge { get; set; }

    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }

    public DateTime? CreationDateFrom { get; set; }
    public DateTime? CreationDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}