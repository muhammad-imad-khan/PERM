using Perm.Common.APIModel;

namespace Perm.Config.CustomFieldMeta.Component.APIModel
{
    public class ReqDeleteCustomFieldMetaModel : IRequestModel
    {
        public long CustomFieldMetaID { get; set; }
    }
}