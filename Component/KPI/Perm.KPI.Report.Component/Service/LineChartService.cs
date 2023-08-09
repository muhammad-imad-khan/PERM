using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.KPI.Report.Component.Service
{
    /// <summary>
    ///     It represents company performance data grouped by departments
    /// </summary>
    [Authenticate]
    public class LineChartService : ServiceBase
    {


        public override string URL => "/api/AttendanceChart";
        public override HttpMethod HttpMethod => HttpMethod.Get;

        protected override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
