using Perm.Model.Abstraction;
using Perm.Model.EmployeeMasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Model.EmployeeMasterData
{
    [Table(name:"Feedback", Schema = "EmployeeMasterData")]
    public class FeedbackRatingModel : ModelBase
    {
        [Key]
        public long RatingID { get; set; }
        public long BusinessPartnerID { get; set; }

        public int RatingMarks { get; set; }

        public string Comments { get; set; }
        public BusinessPartnerModel BusinessPartner { get; set; }


    }
}
