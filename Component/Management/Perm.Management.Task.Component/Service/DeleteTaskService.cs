using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.Management.Tasks.Data.Repository;
using Perm.Management.Tasks.Module.APIModel;

namespace Perm.Management.Tasks.Module.Service
{

    [Authenticate]
    public class DeleteTaskService : ServiceBase
    {
        public override string URL => "/api/Tasks/Delete";
        public override HttpMethod HttpMethod => HttpMethod.Delete;
        private readonly ITaskRepository _taskRepository;
        public DeleteTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqDeleteTaskModel reqModel = CastToObject<ReqDeleteTaskModel>(requestModel);

            await _taskRepository.DeleteTask(reqModel.TaskID);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

    }
}
