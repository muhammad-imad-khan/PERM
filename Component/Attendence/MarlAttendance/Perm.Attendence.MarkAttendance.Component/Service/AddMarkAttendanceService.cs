using Perm.Attendence.MarkAttendance.Data.Repository;
using Perm.Attendence.MarkAttendanceModule.APIModel;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Attendence.MarkAttendanceModule.Service
{
    [Authenticate]
    public class AddMarkAttendanceService : ServiceBase
    {
        public override string URL => "/api/MarkAttendance/Add";
        public override HttpMethod HttpMethod => HttpMethod.Post;
        private readonly IMarkAttendanceRepository _markAttendanceRepository;

        public AddMarkAttendanceService(IMarkAttendanceRepository markAttendanceRepository)
        {
            _markAttendanceRepository = markAttendanceRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddMarkAttendanceModel reqModel = CastToObject<ReqAddMarkAttendanceModel>(requestModel);

            await _markAttendanceRepository.AddMarkAttendance(reqModel);

            return new ResponseModel<T>()
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.MarkAttendanceID
                }
            };
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
