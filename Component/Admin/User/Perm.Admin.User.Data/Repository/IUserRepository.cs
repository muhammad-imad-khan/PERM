using Perm.Admin.Model;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;

namespace Perm.Admin.User.Data.Repository
{
    public interface IUserRepository : IRepository<UserModel>
    {
        public Task AddUser(UserModel userModel);

        public Task UpdateUser(UserModel userModel);

        public Task DeleteUser(long userID);
    }
}