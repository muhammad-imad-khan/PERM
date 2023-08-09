using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [Table(name: "Permission", Schema = "Admin")]
    public class PermissionModel : ModelBase
    {
        [Key]
        public long PermissionID { get; set; }
        public long? UserID { get; set; }
        public long? RoleID { get; set; }
        public long ParamAccessTypeID { get; set; }

        public UserModel User { get; set; }
        public RoleModel Role { get; set; }

        public ApplicationParamDetailModel ParamAccessType { get; set; }
        public List<PermissionDetailModel> PermissionDetail { get; set; }
    }
}