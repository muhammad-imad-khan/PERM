using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.EmployeeMasterData
{
    [Table(name:"PersonalDetail", Schema = "EmployeeMasterData")]
    public class PersonalDetailModel : ModelBase
    {
        [Key]

        public long PersonalDetailsID { get; set; }

        public long BusinessPartnerID { get; set; }

        public string Bio { get; set; }

        public long ParamMaritalStatusID { get; set; }

        public long ParamBloodGroupID { get; set; }

        public string FamilyBackground { get; set; }

        public string HealthDetails { get; set; }

        public string CNIC { get; set; }

        public DateTime CNIC_DateOfIssue { get; set;}

        public DateTime CNIC_ValidUpTo { get; set; }

        public BusinessPartnerModel BusinessPartner { get; set; }


    }
}