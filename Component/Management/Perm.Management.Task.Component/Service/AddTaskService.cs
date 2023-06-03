using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.Management.Tasks.Data.Repository;
using Perm.Management.Tasks.Module.APIModel;

namespace Perm.Management.Tasks.Module.Service
{
    [Authenticate]
    public class AddTaskService : ServiceBase
    {
        public override string URL => "/api/Tasks/Add";
        public override HttpMethod HttpMethod => HttpMethod.Post;
        private readonly ITaskRepository _taskRepository;

        public AddTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddTaskModel reqModel = CastToObject<ReqAddTaskModel>(requestModel);

            await _taskRepository.AddTask(reqModel);

            return new ResponseModel<T>()
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.TaskID
                }
            };
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
