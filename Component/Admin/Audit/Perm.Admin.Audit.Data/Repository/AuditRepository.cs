using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;

namespace Perm.Admin.Audit.Data.Repository
{
    public class AuditRepository : Repository<ViewAuditModel>, IAuditRepository
    {
        public AuditRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }
    }
}