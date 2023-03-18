using Perm.Core.TenantManager.Abstraction;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using System.Linq.Expressions;

namespace Perm.DataAccessLayer.DataRepository.Core.Abstraction;

/// <summary>
/// Generic Repository inherit it to perform data related operations on SQL Server Database. It implements repository pattern.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    ITenantConfigModel TenantConfigModel { get; set; }

    /// <summary>
    /// Get all entities with pagination
    /// </summary>
    /// <param name="applyFilter"></param>
    /// <returns>Use IQueryable as return type</returns>
    PaginationQueryable<T> GetAllWithPagination(bool applyFilter = false);

    /// <summary>
    /// For custom query with pagination like 'where' e.t.c
    /// </summary>
    /// <param name="predict"></param>
    /// <param name="applyFilter"></param>
    /// <returns></returns>
    PaginationQueryable<T> QueryWithPagination(Expression<Func<T, bool>> predict, bool applyFilter = false);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="applyFilter"></param>
    /// <returns></returns>
    IQueryable<T> GetAll(bool applyFilter = false);

    /// <summary>
    /// For custom query
    /// </summary>
    /// <param name="predict"></param>
    /// <param name="applyFilter"></param>
    /// <returns></returns>
    IQueryable<T> Query(Expression<Func<T, bool>> predict, bool applyFilter = false);

    /// <summary>
    /// Add entity to database
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="entityID">Sets EntityID in NotesAttachment Model.</param>
    Task AddAsync(T entity, Func<T, long> entityID = null);

    /// <summary>
    /// Add multiple entities to database
    /// </summary>
    /// <param name="entity"></param>
    Task AddRange(List<T> entity);

    /// <summary>
    /// Update entity to database
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="entityID">Sets EntityID in NotesAttachment Model.</param>
    void Update(T entity, long entityID = 0);

    /// <summary>
    ///     Delete entity from database
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="entityID">Uses EntityID to delete NotesAttachments records.</param>
    /// <param name="setIsDeletedFlag"></param>
    void Delete(T entity, long entityID = 0, bool setIsDeletedFlag = false);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    void DeleteRange(IEnumerable<T> entity);

    /// <summary>
    /// Get first record in sequence
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    Task<T> FirstOrNullAsync(Expression<Func<T, bool>> predict);

    /// <summary>
    /// Update list of entities.
    /// This method also updates / deletes and updates the list records
    /// </summary>
    /// <param name="entityList"></param>
    /// <param name="primaryKey"></param>
    /// <returns></returns>
    Task Update(List<T> entityList, Func<T, long> primaryKey);

    /// <summary>
    /// Read pagination parameter from header
    /// </summary>
    /// <returns></returns>
    (int pageNo, int rowCount, string sortBy, bool isAsc) GetPaginationCriteriaFromHeader();

    IQueryable<T> Query(FormattableString query);
}