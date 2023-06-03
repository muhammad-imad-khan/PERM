using Perm.Common.APIModel;

namespace Perm.Dept.Department.Module.APIModel
{
    public class ReqDeleteDepartmentModel : IRequestModel
    {
        public long DepartmentID { get; set; }
    }
}
