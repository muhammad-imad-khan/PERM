using Perm.Model.Abstraction;
using Perm.Model.Abstraction.Enum;

namespace Perm.DataAccessLayer.DataRepository.Core.Abstraction
{
    public interface ICustomFieldMetaRepository : IRepository<CustomFieldMetaModel>
    {
        /// <summary>
        ///     Get the meta data by entity type ID
        /// </summary>
        /// <param name="entityType">Pass the entity type to get the the meta data by entity type ID</param>
        /// <returns></returns>
        public IQueryable<CustomFieldMetaModel> GetMetaByEntityTypeAsync(EnumEntityType entityType);

        /// <summary>
        /// Add Custom field meta
        /// </summary>
        /// <param name="customFieldMetaModel"></param>
        /// <returns></returns>
        public Task AddCustomFieldMeta(CustomFieldMetaModel customFieldMetaModel);
        /// <summary>
        /// Update Custom field meta
        /// </summary>
        /// <param name="customFieldMetaModel"></param>
        /// <returns></returns>
        public Task UpdateCustomFieldMeta(CustomFieldMetaModel customFieldMetaModel);

        /// <summary>
        /// Delete Custom field meta
        /// </summary>
        /// <param name="customFieldMetaModelID"></param>
        /// <returns></returns>
        public Task DeleteCustomFieldMeta(long customFieldMetaModelID);
    }
}