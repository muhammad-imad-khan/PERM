namespace Perm.DataAccessLayer.DataRepository.Core.Model;

public class PaginationList<T>
{
    /// <summary>
    /// Paged data list
    /// </summary>
    public List<T> List { get; set; }

    /// <summary>
    /// Meta information about applied pagination
    /// </summary>
    public PaginationMeta PaginationMeta { get; set; }

    /// <summary>
    /// Additional Response Data
    /// </summary>
    public object AdditionalData { get; set; }

    /// <summary>
    /// If list has any exception
    /// </summary>
    public Exception Exception { get; set; }
}