using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Management.Tasks.Data.Repository;
using Perm.Model.Management;

namespace Perm.Management.Tasks.Module.Service
{
    [Authenticate]
    public class GetTaskService : ServiceBase
    {
        public override string URL => "/api/Tasks";
        public override HttpMethod HttpMethod => HttpMethod.Get;
        private readonly ITaskRepository _taskRepository;

        public GetTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<TaskModel> entitiesList = await _taskRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)entitiesList
            };
        }
    }
}
