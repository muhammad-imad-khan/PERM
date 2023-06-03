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
    [Table(name: "HealthInsurrance", Schema = "EmployeeMasterData")]

    public class HealthInsurranceModel : ModelBase
    {
        [Key]
        public long HealthInsurranceID { get; set; }
        public long BusinessPartnerID { get; set; }

        public string Provider { get; set; }

        public string InsurranceNumber { get; set; }

        public BusinessPartnerModel BusinessPartner { get; set; }

    }
}
