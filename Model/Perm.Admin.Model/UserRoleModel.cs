using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Admin.Model
{
    [Table(name: "UserRole", Schema = "Admin")]
    public class UserRoleModel : ModelBase
    {
        [Key]
        public long UserRoleID { get; set; }
        public long UserID { get; set; }
        public long RoleID { get; set; }

        public UserModel User { get; set; }
        public RoleModel Role { get; set; }
    }
}