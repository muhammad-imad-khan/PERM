using Perm.Common.APIModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Abstraction
{
    public class CustomFieldModelBase : ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        [NotMapped] public CustomFieldModel CustomField { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped] public List<ReqCustomFieldModel> CustomFields { get; set; }
    }
}