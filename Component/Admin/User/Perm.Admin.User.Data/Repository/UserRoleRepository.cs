using Microsoft.AspNetCore.Http;
using Perm.Admin.Model;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;

namespace Perm.Admin.User.Data.Repository
{
    public class UserRoleRepository : Repository<UserRoleModel>, IUserRoleRepository
    {
        public UserRoleRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }
    }
}