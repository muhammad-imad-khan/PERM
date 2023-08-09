using Microsoft.EntityFrameworkCore;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.KPI.Report.Component.APIModel;
using Perm.Management.Tasks.Data.Repository;
using Perm.Model.EmployeeMasterData;
using Perm.Model.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.KPI.Report.Component.Service
{
    [Authenticate]
    public class TaskCountService : ServiceBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskCountService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public override string URL => "/api/AttendanceChart";
        public override HttpMethod HttpMethod => HttpMethod.Get;

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            List<TaskModel> attendenceList = await _taskRepository.GetAll().ToListAsync();

            List<ResReportModel> groupedData = new List<ResReportModel>();

            if (attendenceList.Count != 0)
            {
                groupedData = attendenceList
                    .GroupBy(g => g.AssignedToID)
                    .Select(s => new ResReportModel
                    {
                        Count = s.Count(),
                        Status = s.FirstOrDefault()?.AssignedTo?.Department?.Name ?? "None"
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
