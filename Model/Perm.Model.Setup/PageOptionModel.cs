using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Setup
{
    [Table(name: "PageOption", Schema = "Setup")]
    public class PageOptionModel : ModelBase
    {
        [Key]
        public long PageOptionID { get; set; }
        public long MenuID { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string PlacementArea { get; set; }
        public bool IsStandard { get; set; }
        public int DisplayOrder { get; set; }

        public MenuModel Menu { get; set; }
    }
}