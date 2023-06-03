using Perm.Attendence.MarkAttendance.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Attendance;

namespace Perm.Attendence.MarkAttendanceModule.Service
{
    [Authenticate]
    public class GetMarkAttendanceService : ServiceBase
    {
        public override string URL => "/api/MarkAttendance";
        public override HttpMethod HttpMethod => HttpMethod.Get;
        private readonly IMarkAttendanceRepository _markAttendanceRepository;

        public GetMarkAttendanceService(IMarkAttendanceRepository markAttendanceRepository)
        {
            _markAttendanceRepository = markAttendanceRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<MarkAttendanceModel> entitiesList = await _markAttendanceRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)entitiesList
            };
        }
    }
}
