using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Config;

namespace Perm.Config.ApplicationParamDetail.Data.Repository
{
    public class ApplicationParamDetailRepository : Repository<ApplicationParamDetailModel>, IApplicationParamDetailRepository
    {
        public ApplicationParamDetailRepository(PermDataContext dataContext, IHttpContextAccessor httpContext) : base(dataContext, httpContext) { }
    }
}