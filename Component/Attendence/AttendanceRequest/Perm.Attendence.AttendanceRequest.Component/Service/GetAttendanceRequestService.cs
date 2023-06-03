using Perm.Attendence.AttendanceRequest.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Attendance;
using Perm.Model.Attendence;

namespace Perm.Attendence.AttendanceRequest.Module.Service
{
    [Authenticate]
    public class GetAttendanceRequestService : ServiceBase
    {
        public override string URL => "/api/AttendanceRequest";
        public override HttpMethod HttpMethod => HttpMethod.Get;
        private readonly IAttendanceRequestRepository _attendanceRequestRepository;

        public GetAttendanceRequestService(IAttendanceRequestRepository attendanceRequestRepository)
        {
            _attendanceRequestRepository = attendanceRequestRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<AttendanceRequestModel> entitiesList = await _attendanceRequestRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)entitiesList
            };
        }
    }
}
