using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Admin;

namespace Perm.Admin.User.Data.Repository
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor, IUserRoleRepository userRoleRepository)
            : base(dataContext, httpContextAccessor)
        {
            _userRoleRepository = userRoleRepository;
        }

        protected override IIncludableQueryable<UserModel, object> IncludeForeignKeys(IQueryable<UserModel> entities)
        {
            return entities.Include(s => s.UserRole).ThenInclude(s => s.Role);
        }

        public async Task AddUser(UserModel userModel)
        {
            await AddAsync(userModel);

            if (userModel.UserRole != null)
            {
                foreach (UserRoleModel item in userModel.UserRole)
                {
                    item.CreatedOn = userModel.CreatedOn;
                    item.CreatedBy = userModel.CreatedBy;
                    item.IsDeleted = userModel.IsDeleted;
                }
            }

            await Context.CommitChangesAsync();
        }

        public async Task UpdateUser(UserModel userModel)
        {
            Update(userModel);

            if (userModel.UserRole != null)
                await AddDeleteUserRoles(userModel);

            await Context.CommitChangesAsync();
        }

        private async Task AddDeleteUserRoles(UserModel userModel)
        {
            UserModel dbUser = Query(s => s.UserID == userModel.UserID).FirstOrDefault();
            List<UserRoleModel> deleteUserRoles = new List<UserRoleModel>();
            if (dbUser.UserRole != null)
                foreach (UserRoleModel dbRoleGroup in dbUser.UserRole)
                {
                    if (userModel.UserRole is not null && !userModel.UserRole.Exists(s => s.UserRoleID == dbRoleGroup.UserRoleID))
                    {
                        deleteUserRoles.Add(new UserRoleModel { UserRoleID = dbRoleGroup.UserRoleID });
                    }
                }

            _userRoleRepository.DeleteRange(deleteUserRoles);

            if (userModel.UserRole != null)
                foreach (UserRoleModel userRole in userModel.UserRole)
                {
                    if (dbUser.UserRole != null && !dbUser.UserRole.Exists(s => s.UserRoleID == userRole.UserRoleID))
                    {
                        await _userRoleRepository.AddAsync(userRole);
                    }
                }
        }

        public async Task DeleteUser(long userID)
        {
            IQueryable<UserRoleModel> userRoles = _userRoleRepository.Query(s => s.UserID == userID);

            Delete(new UserModel { UserID = userID });

            _userRoleRepository.DeleteRange(userRoles);

            await Context.CommitChangesAsync();
        }
    }
}
