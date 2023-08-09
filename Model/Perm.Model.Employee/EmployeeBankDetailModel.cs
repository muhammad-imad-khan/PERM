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
    [Table(name: "EmployeeBankDetail", Schema = "EmployeeMasterData")]

    public class EmployeeBankDetailModel: ModelBase
    {
        [Key]
        public long EmployeeBankDetailID { get; set; }

        public long BusinessPartnerID { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        //public string IBAN { get; set; }

        public string Branch { get; set; }

        public string ProvidentFundAccountNumber { get; set; }

        public BusinessPartnerModel BusinessPartner { get; set; }
    }
}
