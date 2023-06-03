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
    [Table(name: "Level", Schema = "EmployeeMasterData")]

    public class LevelModel : ModelBase
    {
        [Key]

        public long LevelID { get; set; }
        public long BusinessPartnerID { get; set; }


        public string Name { get; set; }

        public string Code { get; set; }

        public string RequiredSkills { get; set; }

        public BusinessPartnerModel BusinessPartner { get; set; }

    }
}
