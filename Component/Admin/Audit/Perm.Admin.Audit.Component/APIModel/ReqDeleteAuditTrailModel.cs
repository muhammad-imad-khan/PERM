using Perm.Common.APIModel;
using System.ComponentModel.DataAnnotations;

namespace Perm.Admin.Audit.Component.APIModel
{
    public class ReqDeleteAuditTrailModel : IRequestModel
    {
        [Required]
        public long AuditTrailID { get; set; }
    }
}