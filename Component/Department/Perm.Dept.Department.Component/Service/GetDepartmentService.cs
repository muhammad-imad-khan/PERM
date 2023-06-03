using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Dept.Department.Data.Repository;
using Perm.Model.Department;

namespace Perm.Dept.Department.Module.Service
{
    [Authenticate]
    public class GetDepartmentService : ServiceBase
    {
        public override string URL => "/api/Department";
        public override HttpMethod HttpMethod => HttpMethod.Get;
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<DepartmentModel> entitiesList = await _departmentRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)entitiesList
            };
        }
    }
}
