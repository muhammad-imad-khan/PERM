using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Attendence;

namespace Perm.Attendence.AttendanceAssistant.Data.Repository
{

    public class AttendanceAssistantRepository : Repository<AttendanceAssistantModel>, IAttendanceAssistantRepository
    {
        public AttendanceAssistantRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }

        public async Task AddAttendanceAssistant(AttendanceAssistantModel AttendanceAssistantModel)
        {
            await AddAsync(AttendanceAssistantModel);
            await Context.CommitChangesAsync();
        }

        public async Task UpdateAttendanceAssistant(AttendanceAssistantModel AttendanceAssistantModel)
        {
            Update(AttendanceAssistantModel);
            await Context.CommitChangesAsync();
        }

        public async Task DeleteAttendanceAssistant(long AttendanceAssistantID)
        {
            Delete(new AttendanceAssistantModel { AttendanceAssistantID = AttendanceAssistantID });
            await Context.CommitChangesAsync();
        }


    }
}
