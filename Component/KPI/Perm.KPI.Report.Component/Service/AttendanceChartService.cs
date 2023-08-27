using Microsoft.EntityFrameworkCore;
using Perm.Attendence.MarkAttendance.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.KPI.Report.Component.APIModel;

namespace Perm.KPI.Report.Component.Service
{
    [Authenticate]
    public class AttendanceChartService : ServiceBase
    {
        private IMarkAttendanceRepository _markAttendanceRepository;

        public AttendanceChartService(IMarkAttendanceRepository markAttendanceRepository)
        {
            _markAttendanceRepository = markAttendanceRepository;
        }

        public override string URL => "/api/AttendanceChart";
        public override HttpMethod HttpMethod => HttpMethod.Get;

        protected async override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            List<Model.Attendance.MarkAttendanceModel> attendenceList = await _markAttendanceRepository.GetAll().ToListAsync();

            List<ResReportModel> groupedData = new List<ResReportModel>();

            if (attendenceList.Count != 0)
            {
                groupedData = attendenceList
                    .Where(x => x.Date.Date >= DateTime.Now.Date)
                    .GroupBy(g => g.ParamAttendenceStatusID)
                    .Select(s => new ResReportModel
                    {
                        Count = s.Count(),
                        Status = s.FirstOrDefault()?.ParamAttendenceStatus?.ParamKey ?? "Unknow"
                    })
                    .OrderBy(o => o.Status)
                    .ToList();
            }

            return new ResponseModel<T>
            {
                Data = (T)(object)groupedData
            };
        }
    }
}
