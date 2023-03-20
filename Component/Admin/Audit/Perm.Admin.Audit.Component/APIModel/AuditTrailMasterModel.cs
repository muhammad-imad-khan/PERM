using Perm.Admin.Audit.Data;

namespace Perm.Admin.Audit.Component.APIModel
{
    public class AuditMasterModel
    {
        public DateTime ChangeOn { get; set; }
        public string ChangeByName { get; set; }
        public int EventID { get; set; }
        public IEnumerable<ViewAuditModel> Detail { get; set; }
    }
}
