using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Attendance;

namespace Perm.Attendence.MarkAttendance.Data.Repository
{
    public interface IMarkAttendanceRepository : IRepository<MarkAttendanceModel>
    {
        public Task AddMarkAttendance(MarkAttendanceModel markAttendanceModel);
        public Task UpdateMarkAttendance(MarkAttendanceModel markAttendanceModel);
        public Task DeleteMarkAttendance(long markAttendanceID);


    }
}