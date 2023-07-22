using Microsoft.AspNetCore.Http;
using Perm.Admin.Permission.Data.Repository.Abstraction;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Admin;

namespace Perm.Admin.Permission.Data.Repository
{
    public class PermissionDetailRepository : Repository<PermissionDetailModel>, IPermissionDetailRepository
    {
        public PermissionDetailRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }
    }
}