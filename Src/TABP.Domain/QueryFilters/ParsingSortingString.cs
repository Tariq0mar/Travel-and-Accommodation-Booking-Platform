using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters;

public class ParsingSortingString
{
    public static List<SortCriteriaRecord> Parse(string? sortString)
    {
        if (string.IsNullOrWhiteSpace(sortString)) 
            return new();

        return sortString
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(part =>
            {
                var segments = part.Split(':');
                var prop = segments[0].Trim();
                var dir = segments.Length > 1 && segments[1].Trim().Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? SortDirection.Desc
                    : SortDirection.Asc;

                return new SortCriteriaRecord(prop, dir);
            }).ToList();
    }
}