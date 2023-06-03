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
    [Table(name: "Education", Schema = "EmployeeMasterData")]

    public class EducationModel : ModelBase
    {
        [Key]
        public long EducationID { get; set; }

        public long BusinessPartnerID { get; set; }

        public string Institute { get; set; }

        public string QualificationProgram { get; set; }

        public DateTime YearOfPassing { get; set; }
    
        
    }
}
