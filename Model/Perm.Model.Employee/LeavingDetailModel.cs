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
    [Table(name: "LeavingDetail", Schema = "EmployeeMasterData")]

    public class LeavingDetailModel : ModelBase  
    {
        [Key]
        public long LeavingDetailID { get; set; }

        public long BusinessPartnerID { get; set; }

        public string LeavingDescription { get; set; }
        public string ReasonForLeaving { get; set; }
        public string NewWorkplace { get; set; }
        public string Feedback { get; set; }

        public string ExitInTo { get; set; }

        public DateTime ResignationLetterDate { get; set; }

        public BusinessPartnerModel BusinessPartner { get; set; }

    }
}
