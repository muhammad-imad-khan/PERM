using Perm.Attendence.AttendanceRequest.Data.Repository;
using Perm.Attendence.AttendanceRequest.Module.APIModel;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Attendence.AttendanceRequest.Module.Service
{
    [Authenticate]
    public class AddAttendanceRequestService : ServiceBase
    {
        public override string URL => "/api/AttendanceRequest/Add";
        public override HttpMethod HttpMethod => HttpMethod.Post;
        private readonly IAttendanceRequestRepository _attendanceRequestRepository;

        public AddAttendanceRequestService(IAttendanceRequestRepository attendanceRequestRepository)
        {
            _attendanceRequestRepository = attendanceRequestRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddAttendanceRequestModel reqModel = CastToObject<ReqAddAttendanceRequestModel>(requestModel);

            await _attendanceRequestRepository.AddAttendanceRequest(reqModel);

            return new ResponseModel<T>()
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.AttendanceRequestID
                }
            };
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
