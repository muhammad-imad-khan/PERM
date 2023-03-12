using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Config
{
    [Table(name: "ApplicationParamDetail", Schema = "Config")]
    public class ApplicationParamDetailModel : ModelBase
    {
        [Key]
        public long ApplicationParamDetailID { get; set; }
        public long ApplicationParamMasterID { get; set; }
        public string ParamKey { get; set; }
        public string ParamValue { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
    }
}