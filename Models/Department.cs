using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM.Models
{
    [Table("Department")]

    public class Department
    {
        [Key]
        [DisplayName("Department ID")]
        [Required]
        public string DeptID { get; set; }

        [DisplayName("Department Number")]
        [Required]
        public string DeptNo { get; set; }

        [DisplayName("Department Name")]
        [Required]
        public string DepartmentName { get; set; }
    }
}
