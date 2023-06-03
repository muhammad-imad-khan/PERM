using Perm.Attendence.MarkAttendance.Data.Repository;
using Perm.Attendence.MarkAttendanceModule.APIModel;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Attendence.MarkAttendanceModule.Service
{
    [Authenticate]
    public class UpdateMarkAttendanceService : ServiceBase
    {
        public override string URL => "/api/MarkAttendance/Update";
        public override HttpMethod HttpMethod => HttpMethod.Put;
        private readonly IMarkAttendanceRepository _markAttendanceRepository;

        public UpdateMarkAttendanceService(IMarkAttendanceRepository markAttendanceRepository)
        {
            _markAttendanceRepository = markAttendanceRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddMarkAttendanceModel reqModel = CastToObject<ReqAddMarkAttendanceModel>(requestModel);

            await _markAttendanceRepository.UpdateMarkAttendance(reqModel);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessMarkAttendance<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

        protected override bool HandleError(IRequestModel requestModel, Exception ex)
        {
            if (ex is PermDuplicateRecordException recordException)
                if (recordException.IndexName == "IX_MarkAttendance_MarkAttendanceName")
                    throw new PermBusinessException("0008", recordException.Value);

            return true;
        }
    }
}
