using System;

namespace Perm.Database.Audit
{
    public class AuditTrailModel
    {
        public string TableName { get; set; }
        public long RecordID { get; set; }
        public string ColumnName { get; set; }
        public int EventID { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangeOn { get; set; }
    }
}