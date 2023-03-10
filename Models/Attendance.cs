using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM.Models
{
    [Table("Attendance")]

    public class Attendance
    {
        [Key]
        [DisplayName("ID")]
        public string AttendanceID { get; set; }
        public string EmployeeID { get; set; }

        [Required]
        [DisplayName("Status")]
        public AttendanceStatus Status { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime AttendanceDate { get; set; }

        [Required]
        [DisplayName("In Time")]
        public DateTime AttendanceInTime { get; set; }

        [Required]
        [DisplayName("Out Time")]
        public DateTime AttendanceOutTime { get; set; }
    }
    public enum AttendanceStatus
    {
        Present,
        Absent,
        OnLeave
    }
}
