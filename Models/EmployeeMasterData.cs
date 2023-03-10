using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM.Models
{
    [Table("EmployeeMasterData")]
    public class EmployeeMasterData
    {

        [Key]
        [DisplayName("Employee ID")]

        public string EmployeeID { get; set; }

        [Required]
        [DisplayName("Department ID")]
        [Range(0, 999999)]
        public int DeptID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string EmployeeFirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string EmployeeLastName { get; set; }

        [Required]
        [DisplayName("Address")]
        public string EmployeeAddress { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [EmailAddress]
        public string EmployeeEmailAddress { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [Phone]
        public string EmployeePhone { get; set; }

        [Required]
        [DisplayName("Joining Date ")]
        public DateTime JoiningDate { get; set; }

        [Required]
        [DisplayName("CNIC")]
        [Range(13, 15, ErrorMessage = "Value for CNIC must be between 13 and 15.")]
        public string EmployeeCNIC { get; set; }

        [Required]
        [DisplayName("Salary")]
        [Range(0, 9999999, ErrorMessage = "Enter the actual salary.")]
        public string EmployeeSalary { get; set; }

        [Required]
        [DisplayName("Designation ")]
        public string EmployeeDesignation { get; set; }

    }
}
