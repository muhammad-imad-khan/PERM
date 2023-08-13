using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Perm.Common;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.KPI.Report.Component.APIModel;
using Perm.KPI.Report.Data.Model;
using Perm.KPI.Report.Data.Repository;

namespace Perm.KPI.Report.Component.Service
{
    [Authenticate]
    public class EmployeePerformanceService : ServiceBase
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IViewEmployeePerformanceRepository _viewEmployeePerformanceRepository;

        public EmployeePerformanceService(IHttpContextAccessor httpContext, IViewEmployeePerformanceRepository viewEmployeePerformanceRepository)
        {
            _httpContext = httpContext;
            _viewEmployeePerformanceRepository = viewEmployeePerformanceRepository;
        }

        public override string URL => "/api/EmployeePerformance";
        public override HttpMethod HttpMethod => HttpMethod.Get;

        private const int TASK_POINT = 5;
        private const int RATING_POINT = 1;
        private const int ATTENDANCE_POINT = 4;

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            _httpContext.HttpContext.Request.Headers.TryGetValue("BusinessPartnerID", out StringValues businessParter);

            long businessParterID = businessParter.ParseTo<long>();

            List<View_EmployeePerformanceModel> employeePerformanceData = await _viewEmployeePerformanceRepository
                .GetAll()
                .Where(s => s.BusinessPartnerID == businessParterID)
                .ToListAsync();

            List<ResEmployeePerformanceModel> groupedData = new List<ResEmployeePerformanceModel>();

            if (employeePerformanceData.Count != 0)
            {
                groupedData = employeePerformanceData
                    .GroupBy(g => g.EmployeeName)
                    .Select(s => new ResEmployeePerformanceModel
                    {
                        EmployeeName = s.Key ?? "None",
                        DepartmentName = s.FirstOrDefault()?.DepartmentName ?? "None",
                        TaskPoint = GetTaskPoint(s),
                        AttendancePoint = GetAttendancePoint(s),
                        RatingPoint = GetRatingPoint(s)
                    })
                    .ToList();
            }

            return new ResponseModel<T>()
            {
                Data = (T)(object)groupedData
            };
        }

        private int GetTaskPoint(IGrouping<string, View_EmployeePerformanceModel> models)
        {
            int points = TASK_POINT;

            foreach (View_EmployeePerformanceModel item in models.Where(s => s.TargetCompletionDate != null && s.Deadline != null))
            {
                if (item.TargetCompletionDate.Value.Date > item.Deadline.Value.Date)
                    points = (int)(points - (TASK_POINT * 0.15));
            }

            return points;
        }

        private int GetAttendancePoint(IGrouping<string, View_EmployeePerformanceModel> models)
        {
            int points = ATTENDANCE_POINT;
            int totalAttendance = models.Where(s => s.ParamAttendenceStatusID != null).Count();
            totalAttendance = totalAttendance == 0 ? 1 : totalAttendance;
            int presentRate = (models.Count(c => c.ParamAttendenceStatusID != null && c.ParamAttendenceStatusID == 12) / totalAttendance) * 100; // present status

            if (presentRate < 70)
            {
                points = (int)(points - (ATTENDANCE_POINT * 0.15));
            }
            else if (presentRate < 60)
            {
                points = (int)(points - (ATTENDANCE_POINT * 0.25));
            }
            else if (presentRate < 50)
            {
                points = (int)(points - (ATTENDANCE_POINT * 0.35));
            }
            else if (presentRate < 40)
            {
                points = (int)(points - (ATTENDANCE_POINT * 0.45));
            }

            return points;
        }

        private double GetRatingPoint(IGrouping<string, View_EmployeePerformanceModel> models)
        {
            int totalRating = models.Where(s => s.RatingMarks != null).Count();
            totalRating = totalRating == 0 ? 1 : totalRating;
            int ratingSum = models.Where(s => s.RatingMarks != null).Sum(s => s.RatingMarks) ?? 0;
            int overallRating = (ratingSum / totalRating);

            return RATING_POINT * overallRating;
        }
    }
}
