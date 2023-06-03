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
    [Table(name: "AttendanceAssistant", Schema = "Attendence")]

    public class AttendanceAssistantModel : ModelBase
    {
        [Key]
        public long AttendanceAssistantID { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string Branch { get; set; }

        public long DepartmentID { get; set; }
    }
}
