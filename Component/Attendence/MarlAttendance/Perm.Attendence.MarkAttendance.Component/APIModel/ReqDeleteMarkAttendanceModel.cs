using Perm.Common.APIModel;

namespace Perm.Attendence.MarkAttendanceModule.APIModel
{
    public class ReqDeleteMarkAttendanceModel : IRequestModel
    {
        public long MarkAttendanceID { get; set; }
    }
}
