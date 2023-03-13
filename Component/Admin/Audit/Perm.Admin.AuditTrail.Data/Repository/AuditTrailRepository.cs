using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;

namespace Perm.Admin.AuditTrail.Data.Repository
{
    public class AuditTrailRepository : Repository<ViewAuditTrailModel>, IAuditTrailRepository
    {
        protected AuditTrailRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }
    }
}