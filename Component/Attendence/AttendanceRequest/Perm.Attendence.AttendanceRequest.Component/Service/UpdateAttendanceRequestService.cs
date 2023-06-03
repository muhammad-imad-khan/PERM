using Perm.Attendence.AttendanceRequest.Data.Repository;
using Perm.Attendence.AttendanceRequest.Module.APIModel;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Attendence.AttendanceRequest.Module.Service
{
    [Authenticate]
    public class UpdateAttendanceRequestService : ServiceBase
    {
        public override string URL => "/api/AttendanceRequest/Update";
        public override HttpMethod HttpMethod => HttpMethod.Put;
        private readonly IAttendanceRequestRepository _attendanceRequestRepository;

        public UpdateAttendanceRequestService(IAttendanceRequestRepository attendanceRequestRepository)
        {
            _attendanceRequestRepository = attendanceRequestRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddAttendanceRequestModel reqModel = CastToObject<ReqAddAttendanceRequestModel>(requestModel);

            await _attendanceRequestRepository.UpdateAttendanceRequest(reqModel);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessAttendanceRequest<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

        protected override bool HandleError(IRequestModel requestModel, Exception ex)
        {
            if (ex is PermDuplicateRecordException recordException)
                if (recordException.IndexName == "IX_AttendanceRequest_AttendanceRequestName")
                    throw new PermBusinessException("0008", recordException.Value);

            return true;
        }
    }
}
