using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Perm.Model.Abstraction;

namespace Perm.Model.Department
{
    [Table(name: "Department", Schema = "Dept")]

    public class DepartmentModel : ModelBase
    {
        [Key]
        public long DepartmentID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
