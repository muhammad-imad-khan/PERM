using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Admin;

namespace Perm.Admin.Permission.Data.Repository.Abstraction
{
    public interface IPermissionRepository : IRepository<PermissionModel>
    {
        public Task AddPermission(PermissionModel permission);
        public Task UpdatePermission(PermissionModel permission);
        public Task DeletePermission(long permissionID);
    }
}
