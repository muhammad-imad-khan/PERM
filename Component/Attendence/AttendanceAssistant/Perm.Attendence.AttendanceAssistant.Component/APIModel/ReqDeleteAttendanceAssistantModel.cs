using Perm.Common.APIModel;

namespace Perm.Attendence.AttendanceAssistant.Module.APIModel
{
    public class ReqDeleteAttendanceAssistantModel : IRequestModel
    {
        public long AttendanceAssistantID { get; set; }
    }
}
