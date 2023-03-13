using Perm.Common.APIModel;
using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Admin.AuditTrail.Data
{
    [Table(name: "View_AuditTrail", Schema = "Admin")]
    public class ViewAuditTrailModel : ModelBase
    {
        [Key]
        public long AuditTrailID { get; set; }

        [NotMapped]
        public string ChangeID => ChangeOn.ToString("yyMMddhhmmssffff");

        public string EntityType { get; set; }
        public string TableName { get; set; }
        public long RecordID { get; set; }
        public string ColumnName { get; set; }
        public string TableColumnName { get; set; }
        public int EventID { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangeOn { get; set; }
        public string ChangeByName { get; set; }
        public string DataType { get; set; }
        [NotMapped]
        public new bool IsDeleted { get; set; }

        [NotMapped]
        public new DateTime CreatedOn { get; set; }

        [NotMapped]
        public new long CreatedBy { get; set; }
        
        [NotMapped]
        public AuditAttribute AuditAttribute { get; set; }

        public override string ToString()
        {
            return $"{TableName}.{ColumnName}";
        }
    }
}