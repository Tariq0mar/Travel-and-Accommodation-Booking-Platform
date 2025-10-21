using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters;

public record SortCriteriaRecord(string PropertyName, SortDirection Direction);