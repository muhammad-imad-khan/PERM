using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Management.Tasks.Data.Repository
{
    public interface ITaskRepository : IRepository<TaskModel>
    {
        public Task AddTask(TaskModel taskModel);
        public Task UpdateTask(TaskModel taskModel);
        public Task DeleteTask(long taskID);


    }
}
