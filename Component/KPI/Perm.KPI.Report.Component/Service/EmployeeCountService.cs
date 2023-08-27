using Microsoft.EntityFrameworkCore;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;
using Perm.KPI.Report.Component.APIModel;
using Perm.Model.EmployeeMasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.KPI.Report.Component.Service
{
    [Authenticate]
    public class EmployeeCountService : ServiceBase
    {
        private readonly IBusinessPartnerRepository _businessPartnerRepository;

        public EmployeeCountService(IBusinessPartnerRepository businessPartnerRepository)
        {
            _businessPartnerRepository = businessPartnerRepository;
        }

        public override string URL => "/api/EmployeeCount";
        public override HttpMethod HttpMethod => HttpMethod.Get;

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            List<BusinessPartnerModel> attendenceList = await _businessPartnerRepository.GetAll().ToListAsync();

            List<ResReportModel> groupedData = new List<ResReportModel>();

            if (attendenceList.Count != 0)
            {
                groupedData = attendenceList
                    .GroupBy(g => g.DepartmentID)
                    .Select(s => new ResReportModel
                    {
                        Count = s.Count(),
                        Status = s.FirstOrDefault()?.Department?.Name ?? "Unknown"
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