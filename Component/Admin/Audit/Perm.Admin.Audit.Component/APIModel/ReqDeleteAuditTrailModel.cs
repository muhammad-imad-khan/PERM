using Perm.Common.APIModel;
using System.ComponentModel.DataAnnotations;

namespace Perm.Admin.Audit.Component.APIModel
{
    public class ReqDeleteAuditModel : IRequestModel
    {
        [Required]
        public long AuditID { get; set; }
    }
}