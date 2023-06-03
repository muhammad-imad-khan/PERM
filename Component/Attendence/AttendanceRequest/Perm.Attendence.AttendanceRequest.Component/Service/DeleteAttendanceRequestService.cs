using Perm.Attendence.AttendanceRequest.Data.Repository;
using Perm.Attendence.AttendanceRequest.Module.APIModel;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;

namespace Perm.Attendence.AttendanceRequest.Module.Service
{
    [Authenticate]
    public class DeleteAttendanceRequestService : ServiceBase
    {
        public override string URL => "/api/AttendanceRequest/Delete";
        public override HttpMethod HttpMethod => HttpMethod.Delete;
        private readonly IAttendanceRequestRepository _attendanceRequestRepository;
        public DeleteAttendanceRequestService(IAttendanceRequestRepository attendanceRequestRepository)
        {
            _attendanceRequestRepository = attendanceRequestRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqDeleteAttendanceRequestModel reqModel = CastToObject<ReqDeleteAttendanceRequestModel>(requestModel);

            await _attendanceRequestRepository.DeleteAttendanceRequest(reqModel.AttendanceRequestID);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

    }
}
