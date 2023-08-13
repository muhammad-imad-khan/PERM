using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.Management.Tasks.Data.Enum;
using Perm.Management.Tasks.Data.Repository;
using Perm.Management.Tasks.Module.APIModel;
using Perm.Model.Management;

namespace Perm.Management.Tasks.Component.Service
{
    [Authenticate]
    public class MarkAsDoneTaskService : ServiceBase
    {
        public override string URL => "/api/Tasks/MarkAsDone";
        public override HttpMethod HttpMethod => HttpMethod.Put;
        private readonly ITaskRepository _taskRepository;

        public MarkAsDoneTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddTaskModel reqModel = CastToObject<ReqAddTaskModel>(requestModel);

            TaskModel dbTask = await _taskRepository.FirstOrNullAsync(t => t.TaskID == reqModel.TaskID);

            dbTask.TargetCompletionDate = DateTime.Now;
            dbTask.ParamTaskStatusID = (int)EnumTaskStatus.Completed;

            await _taskRepository.UpdateTask(dbTask);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessTask<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

        protected override bool HandleError(IRequestModel requestModel, Exception ex)
        {
            if (ex is PermDuplicateRecordException recordException)
                if (recordException.IndexName == "IX_Task_TaskName")
                    throw new PermBusinessException("0008", recordException.Value);

            return true;
        }
    }
}
