using Perm.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Model.EmployeeMasterData
{
    [Table(name: "BusinessPartner", Schema = "EmployeeMasterData")]
    public class BusinessPartnerModel : ModelBase
    {
        [Key]
        public long BusinessPartnerID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string Gender { get; set; }

        #region One To One Relationships

        public PersonalDetailModel PersonalDetail { get; set; }
        public LeavingDetailModel LeavingDetail { get; set; }
        public HealthInsurranceModel HealthInsurance { get; set; }
        public LevelModel Level { get; set; }

        #endregion

        #region One To Many Relationships

        public List<EducationModel> Education { get; set; }
        public List<EmployeeShiftModel> Shift { get; set; }
        public List<EmployeeContactModel> Contact { get; set; }
        public List<EmployeeAddressModel> Address { get; set; }
        public List<EmployeeBankDetailModel> BankDetail { get; set; }
        public List<HistoryInCompanyModel> HistoryInCompany { get; set; }

        #endregion
    }
}
