using Perm.Model.Abstraction;
using Perm.Model.EmployeeMasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Model.Attendance
{
    [Table(name: "MarkAttendance", Schema = "Attendence")]

    public class MarkAttendanceModel : ModelBase
    {
        [Key]

        public long MarkAttendanceID { get; set; }

        public long? BusinessPartnerID { get; set; }

        public long? ParamAttendenceStatusID { get; set; }

        public long? ParamShiftID { get; set; }

        public bool IsLateEntry { get; set; }

        public bool IsEarlyExit { get; set; }
        public bool IsEmergencyExit { get; set; }

        public DateTime Date { get; set; }

        public ApplicationParamDetailModel ParamAttendenceStatus { get; set; }
        public ApplicationParamDetailModel ParamShift { get; set; }
        public BusinessPartnerModel BusinessPartner { get; set; }
    }
}
