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
    [Table(name: "EmployeeContact", Schema = "EmployeeMasterData")]

    public class EmployeeContactModel : ModelBase
    {
        [Key]
        public long EmployeeContactID { get; set; }

        public long BusinessPartnerID { get; set; }

        public string Number { get; set; }

        public string EmergencyNumber { get; set; }

        public string RelationToEmergence { get; set; }

        [EmailAddress]
        public string PersonalEmail { get; set; }

        [EmailAddress]
        public string CompanyEmail { get; set; }

        [EmailAddress]
        public string PreferredEmail { get; set; }

    }
}
