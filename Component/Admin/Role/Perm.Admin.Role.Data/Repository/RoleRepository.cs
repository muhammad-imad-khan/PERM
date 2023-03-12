using Microsoft.AspNetCore.Http;
using Perm.Admin.Model;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;

namespace Perm.Admin.Role.Data.Repository
{
    public class RoleRepository : Repository<RoleModel>, IRoleRepository
    {
        protected RoleRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }

        public async Task AddRole(RoleModel roleModel)
        {
            await AddAsync(roleModel);
            await Context.CommitChangesAsync();
        }

        public async Task UpdateRole(RoleModel roleModel)
        {
            Update(roleModel);
            await Context.CommitChangesAsync();
        }

        public async Task DeleteRole(long roleID)
        {
            Delete(new RoleModel { RoleID = roleID });
            await Context.CommitChangesAsync();
        }
    }
}