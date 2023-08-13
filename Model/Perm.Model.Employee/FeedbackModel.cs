using Perm.Model.Abstraction;
using Perm.Model.EmployeeMasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Employee
{
    [Table(name: "Feedback", Schema = "EmployeeMasterData")]
    public class FeedbackModel : ModelBase
    {
        [Key]
        public long FeedbackID { get; set; }
        public long BusinessPartnerID { get; set; }
        public int RatingMarks { get; set; }
        public string Comments { get; set; }

        public BusinessPartnerModel BusinessPartner { get; set; }
    }
}