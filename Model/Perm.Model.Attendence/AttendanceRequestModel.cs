using Perm.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Model.Attendence
{
    [Table(name: "AttendanceRequest", Schema = "Attendence")]

    public class AttendanceRequestModel : ModelBase
    {
        [Key]

        public long AttendanceRequestID { get; set; }

        public long? EmployeeID { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool HalfDay { get; set; }

        public string Reason { get; set; }
    }
}
