using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Admin;

namespace Perm.Admin.User.Data.Repository
{
    public class UserRoleRepository : Repository<UserRoleModel>, IUserRoleRepository
    {
        public UserRoleRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }
    }
}