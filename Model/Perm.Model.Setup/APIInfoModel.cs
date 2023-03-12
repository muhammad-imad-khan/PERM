using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Setup
{
    [Table(name: "APIInfo", Schema = "Setup")]
    public class APIInfoModel : ModelBase
    {
        [Key]
        public long APIInfo { get; set; }
        public long PageOptionID { get; set; }
        public string APIEndPoint { get; set; }
        public string HttpHeaders { get; set; }
    }
}
