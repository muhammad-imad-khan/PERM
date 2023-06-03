using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Attendence;

namespace Perm.Attendence.AttendanceAssistant.Data.Repository
{
    public interface IAttendanceAssistantRepository : IRepository<AttendanceAssistantModel>
    {
        public Task AddAttendanceAssistant(AttendanceAssistantModel attendanceAssistantModel);
        public Task UpdateAttendanceAssistant(AttendanceAssistantModel attendanceAssistantModel);
        public Task DeleteAttendanceAssistant(long attendanceAssistantID);


    }
}