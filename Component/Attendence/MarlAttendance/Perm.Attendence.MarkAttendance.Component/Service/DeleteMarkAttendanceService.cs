using Perm.Attendence.MarkAttendance.Data.Repository;
using Perm.Attendence.MarkAttendanceModule.APIModel;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;

namespace Perm.Attendence.MarkAttendanceModule.Service
{
    [Authenticate]
    public class DeleteMarkAttendanceService : ServiceBase
    {
        public override string URL => "/api/MarkAttendance/Delete";
        public override HttpMethod HttpMethod => HttpMethod.Delete;
        private readonly IMarkAttendanceRepository _markAttendanceRepository;
        public DeleteMarkAttendanceService(IMarkAttendanceRepository markAttendanceRepository)
        {
            _markAttendanceRepository = markAttendanceRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqDeleteMarkAttendanceModel reqModel = CastToObject<ReqDeleteMarkAttendanceModel>(requestModel);

            await _markAttendanceRepository.DeleteMarkAttendance(reqModel.MarkAttendanceID);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

    }
}
