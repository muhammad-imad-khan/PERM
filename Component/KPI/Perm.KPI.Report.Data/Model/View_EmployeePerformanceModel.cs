using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.KPI.Report.Data.Model
{
    [Table(name: "View_EmployeePerformance", Schema = "KPI")]

    public class View_EmployeePerformanceModel : ModelBase
    {
        [Key]
        public long BusinessPartnerID { get; set; }
        public string Code { get; set; }

        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }

        public string Gender { get; set; }

        public int? RatingMarks { get; set; }

        public long? ParamAttendenceStatusID { get; set; }
        public bool? IsLateEntry { get; set; }

        public bool? IsEarlyExit { get; set; }
        public DateTime? Deadline { get; set; }

        public DateTime? TargetCompletionDate { get; set; }





    }
}
