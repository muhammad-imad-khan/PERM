using Perm.Common.APIModel;

namespace Perm.Attendence.AttendanceRequest.Module.APIModel
{
    public class ReqDeleteAttendanceRequestModel :  IRequestModel
    {
        public long AttendanceRequestID { get; set; }
    }
}
