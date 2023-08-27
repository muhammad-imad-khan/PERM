using Perm.Model.Abstraction;
using Perm.Model.EmployeeMasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Admin
{
    [Table(name: "User", Schema = "Admin")]
    public class UserModel : ModelBase
    {
        [Key]
        public long UserID { get; set; }
        public string LoginID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public long BusinessPartnerID { get; set; }
        public List<UserRoleModel> UserRole { get; set; }
        public BusinessPartnerModel BusinessPartner { get; set; }
    }
}