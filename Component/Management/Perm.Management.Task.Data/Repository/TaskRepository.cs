using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Management;

namespace Perm.Management.Tasks.Data.Repository
{
    public class TaskRepository : Repository<TaskModel>, ITaskRepository
    {
        public TaskRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }

        public async Task AddTask(TaskModel taskModel)
        {
            await AddAsync(taskModel);
            await Context.CommitChangesAsync();
        }

        public async Task UpdateTask(TaskModel taskModel)
        {
            Update(taskModel);
            await Context.CommitChangesAsync();
        }

        public async Task DeleteTask(long taskID)
        {
            Delete(new TaskModel { TaskID = taskID });
            await Context.CommitChangesAsync();
        }


    }
}
