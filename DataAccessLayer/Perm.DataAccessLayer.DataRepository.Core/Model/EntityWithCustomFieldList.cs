using Perm.Model.Abstraction;

namespace Perm.DataAccessLayer.DataRepository.Core.Model
{
    public class EntityWithCustomFieldList<T> : PaginationList<T>
    {
        public List<CustomFieldMetaModel> CustomFieldMeta { get; set; }
    }
}