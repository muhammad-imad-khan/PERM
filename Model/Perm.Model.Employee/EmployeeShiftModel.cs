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
    [Table(name: "EmployeeShift", Schema = "EmployeeMasterData")]

    public class EmployeeShiftModel: ModelBase
    {
        [Key]

        public long EmployeeShiftID { get; set; }

        public long BusinessPartnerID { get; set; }

        public long ParamShiftID { get; set; }

        public string AnnualLeaves { get; set; }

    }
}
