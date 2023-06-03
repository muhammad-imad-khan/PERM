using Perm.Attendence.AttendanceAssistant.Data.Repository;
using Perm.Attendence.AttendanceAssistant.Module.APIModel;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;

namespace Perm.Attendence.AttendanceAssistant.Module.Service
{

    [Authenticate]
    public class DeleteAttendanceAssistantService : ServiceBase
    {
        public override string URL => "/api/AttendanceAssistant/Delete";
        public override HttpMethod HttpMethod => HttpMethod.Delete;
        private readonly IAttendanceAssistantRepository _attendanceAssistantRepository;
        public DeleteAttendanceAssistantService(IAttendanceAssistantRepository attendanceAssistantRepository)
        {
            _attendanceAssistantRepository = attendanceAssistantRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqDeleteAttendanceAssistantModel reqModel = CastToObject<ReqDeleteAttendanceAssistantModel>(requestModel);

            await _attendanceAssistantRepository.DeleteAttendanceAssistant(reqModel.AttendanceAssistantID);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

    }
}
