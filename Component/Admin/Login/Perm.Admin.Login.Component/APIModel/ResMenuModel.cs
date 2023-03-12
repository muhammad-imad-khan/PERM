using Newtonsoft.Json;
using Perm.Model.Setup;

namespace Perm.Admin.Login.Component.APIModel
{
    public class ResMenuModel : MenuModel
    {
        public List<ResMenuModel> SubMenu { get; set; }

        [JsonIgnore]
        public new MenuModel ParentMenu { get; set; }

        [JsonIgnore]
        public new long? ParentMenuID { get; set; }

        public new List<ResPageOptionModel> PageOption { get; set; }

        public new bool? IsDeleted { get; set; }

        public new DateTime? CreatedOn { get; set; }

        public new long? CreatedBy { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Link}";
        }
    }
}