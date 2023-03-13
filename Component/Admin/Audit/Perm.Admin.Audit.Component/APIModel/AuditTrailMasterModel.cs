using Perm.Admin.AuditTrail.Data;

namespace Perm.Admin.Audit.Component.APIModel
{
    public class AuditTrailMasterModel
    {
        public DateTime ChangeOn { get; set; }
        public string ChangeByName { get; set; }
        public int EventID { get; set; }
        public IEnumerable<ViewAuditTrailModel> Detail { get; set; }
    }
}
