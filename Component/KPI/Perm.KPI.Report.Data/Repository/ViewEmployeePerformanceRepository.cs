using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.KPI.Report.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.KPI.Report.Data.Repository
{
    public class ViewEmployeePerformanceRepository : Repository<View_EmployeePerformanceModel>, IViewEmployeePerformanceRepository
    {
        public ViewEmployeePerformanceRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }
    }
}
