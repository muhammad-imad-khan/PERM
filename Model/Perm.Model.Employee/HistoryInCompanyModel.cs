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
    [Table(name: "HistoryInCompany", Schema = "EmployeeMasterData")]

    public class HistoryInCompanyModel : ModelBase
    {
        [Key]

        public long HistoryInCompanyID { get; set; }
        public long BusinessPartnerID { get; set; }

        public string Branch { get; set; }

        public long DepartmentID { get; set; }

        public long LevelID { get; set; }

        public decimal Salary { get; set; }

        public string Company { get; set; }

        public BusinessPartnerModel BusinessPartner { get; set; }
    }
}
