using Perm.Model.Abstraction;
using Perm.Model.Abstraction.Enum;

namespace Perm.DataAccessLayer.DataRepository.Core.Abstraction
{
    public interface ICustomFieldRepository : IRepository<CustomFieldModel>
    {
        /// <summary>
        ///     Get custom field record by Entity ID
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityTypeID"></param>
        /// <returns></returns>
        public Task<CustomFieldModel> GetByEntityIDAsync(EnumEntityType entityType, long entityTypeID);

        /// <summary>
        ///     Get all the record belongs to an entity
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public IQueryable<CustomFieldModel> GetByEntityTypeIDAsync(EnumEntityType entityType);

        /// <summary>
        ///     Add custom field related to its entity ID
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityID"></param>
        /// <param name="entityType"></param>
        public Task SaveAsync(CustomFieldModel entity, long entityID, EnumEntityType entityType);

        /// <summary>
        ///     Delete custom field related to its entity ID
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="entityType"></param>
        public Task DeleteAsync(long entityID, EnumEntityType entityType);
    }
}