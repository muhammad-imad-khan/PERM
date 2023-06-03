using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Attendence;

namespace Perm.Attendence.AttendanceRequest.Data.Repository
{
    public interface IAttendanceRequestRepository : IRepository<AttendanceRequestModel>
    {
        public Task AddAttendanceRequest(AttendanceRequestModel attendanceRequestModel);
        public Task UpdateAttendanceRequest(AttendanceRequestModel attendanceRequestModel);
        public Task DeleteAttendanceRequest(long attendanceRequestID);


    }
}
