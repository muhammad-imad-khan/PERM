using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        protected override IIncludableQueryable<TaskModel, object> IncludeForeignKeys(IQueryable<TaskModel> entities)
        {
            return entities.Include(i => i.AssignedTo).ThenInclude(i => i.Department);
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
