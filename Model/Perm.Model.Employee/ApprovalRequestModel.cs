using Perm.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Model.EmployeeMasterData
{
    [Table(name: "ApprovalRequest" , Schema = "EmployeeMasterData")]
    public class ApprovalRequestModel : ModelBase
    {
        [Key]
        public long ApprovalRequestID { get; set; }

        public long ParamEntityTypeID { get; set; }

        public long ApproverID { get; set; }

        public DateTime DateAttribute1 { get; set; }
        public DateTime DateAttribute2 { get; set; }
        public decimal AmmountAttribute1 { get; set; }
        public decimal AmmountAttribute2 { get; set; }

        public string TextAttribute1 { get; set; }
        public string TextAttribute2 { get; set; }

    }
}
