using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Admin
{
    [Table(name: "Role", Schema = "Admin")]
    public class RoleModel : ModelBase
    {
        [Key]
        public long RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}