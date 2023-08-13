using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Management;

namespace Perm.Management.Tasks.Data.Repository
{
    public interface ITaskRepository : IRepository<TaskModel>
    {
        public Task AddTask(TaskModel taskModel);
        public Task UpdateTask(TaskModel taskModel);
        public Task DeleteTask(long taskID);
    }
}
