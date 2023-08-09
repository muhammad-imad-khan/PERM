using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Setup
{
    [Table(name: "Menu", Schema = "Setup")]
    public class MenuModel : ModelBase
    {
        [Key]
        public long MenuID { get; set; }
        public long? ParentMenuID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public long? ParamEntityTypeID { get; set; }
        public int SortNo { get; set; }
        public bool IsAdmin { get; set; }

        public MenuModel ParentMenu { get; set; }
        public ApplicationParamDetailModel ParamEntityType { get; set; }
        public List<PageOptionModel> PageOption { get; set; }
    }
}