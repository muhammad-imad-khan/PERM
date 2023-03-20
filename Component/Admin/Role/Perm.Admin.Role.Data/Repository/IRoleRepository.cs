using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Admin;

namespace Perm.Admin.Role.Data.Repository
{
    public interface IRoleRepository : IRepository<RoleModel>
    {
        public Task AddRole(RoleModel roleModel);

        public Task UpdateRole(RoleModel roleModel);

        public Task DeleteRole(long roleID);
    }
}