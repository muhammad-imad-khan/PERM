using Perm.Attendence.AttendanceAssistant.Data.Repository;
using Perm.Attendence.AttendanceAssistant.Module.APIModel;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Attendence.AttendanceAssistant.Module.Service
{
    [Authenticate]
    public class AddAttendanceAssistantService : ServiceBase
    {
        public override string URL => "/api/AttendanceAssistant/Add";
        public override HttpMethod HttpMethod => HttpMethod.Post;
        private readonly IAttendanceAssistantRepository _attendanceAssistantRepository;

        public AddAttendanceAssistantService(IAttendanceAssistantRepository attendanceAssistantRepository)
        {
            _attendanceAssistantRepository = attendanceAssistantRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddAttendanceAssistantModel reqModel = CastToObject<ReqAddAttendanceAssistantModel>(requestModel);

            await _attendanceAssistantRepository.AddAttendanceAssistant(reqModel);

            return new ResponseModel<T>()
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.AttendanceAssistantID
                }
            };
        }

        public Task<ResponseModel<T>> ProcessAttendanceAssistant<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

        protected override bool HandleError(IRequestModel requestModel, Exception ex)
        {
            if (ex is PermDuplicateRecordException recordException)
                if (recordException.IndexName == "IX_AttendanceAssistant_AttendanceAssistantName")
                    throw new PermBusinessException("0008", recordException.Value);

            return true;
        }
    }
}
