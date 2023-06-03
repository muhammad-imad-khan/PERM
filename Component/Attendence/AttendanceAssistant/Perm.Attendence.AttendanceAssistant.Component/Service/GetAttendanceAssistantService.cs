using Perm.Attendence.AttendanceAssistant.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Attendance;
using Perm.Model.Attendence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Attendence.AttendanceAssistant.Module.Service
{
    [Authenticate]
    public class GetAttendanceAssistantService : ServiceBase
    {
        public override string URL => "/api/AttendanceAssistant";
        public override HttpMethod HttpMethod => HttpMethod.Get;
        private readonly IAttendanceAssistantRepository _attendanceAssistantRepository;

        public GetAttendanceAssistantService(IAttendanceAssistantRepository attendanceAssistantRepository)
        {
            _attendanceAssistantRepository = attendanceAssistantRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<AttendanceAssistantModel> entitiesList = await _attendanceAssistantRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)entitiesList
            };
        }
    }
}
