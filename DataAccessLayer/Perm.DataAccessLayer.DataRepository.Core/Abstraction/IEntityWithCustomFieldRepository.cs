using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Abstraction.Enum;

namespace Perm.DataAccessLayer.DataRepository.Core.Abstraction
{
    /// <summary>
    ///     This interface is designed for business requirement, to add / remove the custom fields on UI. User can add/remove the
    ///     columns from view.
    ///     If you are required to write the repository that will need above functionality, then you will need your repository
    ///     to be implement this interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityWithCustomFieldRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        ///     Get data with addition of custom fields. It will also return the meta of custom field.
        ///     This method will also insure the Unit of Work, and process the database operation in Unit of Work
        /// </summary>
        /// <param name="entityType">Entity type ID</param>
        /// <param name="getProp">Pass the primary key property as lambda expression to identify the EntityID</param>
        /// <returns></returns>
        public Task<EntityWithCustomFieldList<T>> GetAllWithCustomFieldAsync(EnumEntityType entityType,
            Func<T, long> getProp);

        /// <summary>
        ///     Add record to database with addition of custom field. A custom field object is required to store the data
        ///     This method will also insure the Unit of Work, and process the database operation in  Unit of Work
        /// </summary>
        /// <param name="entity">
        ///     Pass the entity to be add, this entity must have custom field object to store the custom field
        ///     data
        /// </param>
        /// <param name="entityID"></param>
        /// <param name="entityType">Entity type for the custom field</param>
        public Task AddWithCustomFieldAsync(T entity, Func<T, long> entityID, EnumEntityType entityType);

        /// <summary>
        ///     Update record to database with addition of custom field. A custom field object is required to store the data
        ///     This method will also insure the Unit of Work, and process the database operation in  Unit of Work
        /// </summary>
        /// <param name="entity">
        ///     Pass the entity to be add, this entity must have custom field object to store the custom field
        ///     data
        /// </param>
        /// <param name="entityID"></param>
        /// <param name="entityType">Entity type for the custom field</param>
        public Task UpdateWithCustomFieldAsync(T entity, long entityID, EnumEntityType entityType);

        /// <summary>
        ///     Delete record from database with addition of custom field. It will not permanently delete the record from database, it will only flag the IsDeleted column to '0'
        ///     This method will also insure the Unit of Work, and process the database operation in  Unit of Work
        /// </summary>
        /// <param name="entity">
        ///     Pass the entity to be add, this entity must have custom field object to store the custom field
        ///     data
        /// </param>
        /// <param name="getProp">Pass the primary key property as lambda expression to identify the EntityID</param>
        /// <param name="entityType">Entity type for the custom field</param>
        public Task DeleteWithCustomFieldAsync(T entity, long getProp, EnumEntityType entityType);
    }
}