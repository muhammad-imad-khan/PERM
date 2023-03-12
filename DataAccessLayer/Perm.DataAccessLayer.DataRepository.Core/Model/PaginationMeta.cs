namespace Perm.DataAccessLayer.DataRepository.Core.Model;

/// <summary>
/// Meta information about applied pagination
/// </summary>
public class PaginationMeta
{
    /// <summary>
    /// Total number of records without pagination
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Current page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Rows to be display for current data list
    /// </summary>
    public int RowCount { get; set; }

    /// <summary>
    /// Sort expression
    /// </summary>
    public string SortBy { get; set; }

    /// <summary>
    /// Sort order (Asc / Desc)
    /// </summary>
    public string SortOrder { get; set; }
}