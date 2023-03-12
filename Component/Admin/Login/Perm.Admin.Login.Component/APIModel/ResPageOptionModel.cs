using Perm.Model.Setup;

namespace Perm.Admin.Login.Component.APIModel
{
    public class ResPageOptionModel : PageOptionModel
    {
        public new long? PageOptionID
        {
            get;
            set;
        }

        public new long? MenuID
        {
            get;
            set;
        }

        public new bool? IsDeleted { get; set; }

        public new DateTime? CreatedOn { get; set; }

        public new long? CreatedBy { get; set; }
    }
}