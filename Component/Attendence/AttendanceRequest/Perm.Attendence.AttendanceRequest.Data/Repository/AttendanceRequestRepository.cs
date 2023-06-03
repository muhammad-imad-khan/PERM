using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Attendance;
using Perm.Model.Attendence;

namespace Perm.Attendence.AttendanceRequest.Data.Repository
{ 
        public class AttendanceRequestRepository : Repository<AttendanceRequestModel>, IAttendanceRequestRepository
    {
        public AttendanceRequestRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }

        public async Task AddAttendanceRequest(AttendanceRequestModel attendanceRequestModel)
        {
            await AddAsync(attendanceRequestModel);
            await Context.CommitChangesAsync();
        }

        public async Task UpdateAttendanceRequest(AttendanceRequestModel attendanceRequestModel)
        {
            Update(attendanceRequestModel);
            await Context.CommitChangesAsync();
        }

        public async Task DeleteAttendanceRequest(long attendanceRequestID)
        {
            Delete(new AttendanceRequestModel { AttendanceRequestID = attendanceRequestID });
            await Context.CommitChangesAsync();
        }


    }
}
