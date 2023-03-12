using Microsoft.EntityFrameworkCore;

namespace Perm.DataAccessLayer.DataRepository.Core.Model;

/// <summary>
/// If we are queering the paginated data then we need to use this type
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginationQueryable<T>
{
    /// <summary>
    /// Generate the list of pagination queryable list, it also set the metadata of pagination
    /// </summary>
    /// <returns></returns>
    public async Task<PaginationList<T>> ToPaginationListAsync()
    {
        List<T> listAsync = new();
        Exception exception = null;
        if (PagedQueryable is not null)
        {
            try
            {
                listAsync = await PagedQueryable.ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                exception = ex;
            }
        }

        if (listAsync.Count == 1)
        {
            if (listAsync[0] == null)
            {
                //if List only have one row of null values then list should be null.
                listAsync = new();
            }
        }

        PaginationList<T> ts = new PaginationList<T>
        {
            List = listAsync,
            Exception = exception,
            PaginationMeta = GetPaginationMeta()
        };

        return ts;
    }

    /// <summary>
    /// Get pagination info
    /// </summary>
    /// <returns></returns>
    public PaginationMeta GetPaginationMeta()
    {
        return new PaginationMeta
        {
            PageNumber = PageNumber,
            RowCount = RowSize,
            SortBy = SortBy,
            SortOrder = SortOrder,
            Count = OriginalQueryable.Count(),
        };
    }

    public IQueryable<T> PagedQueryable { get; set; }

    /// <summary>
    /// Return original queryable object
    /// </summary>
    public IQueryable<T> OriginalQueryable { get; set; }

    internal int PageNumber { get; set; }
    internal int RowSize { get; set; }
    internal string SortBy { get; set; }
    internal string SortOrder { get; set; }
}
