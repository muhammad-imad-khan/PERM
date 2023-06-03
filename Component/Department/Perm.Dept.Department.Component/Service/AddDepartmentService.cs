using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.Dept.Department.Data.Repository;
using Perm.Dept.Department.Module.APIModel;

namespace Perm.Dept.Department.Module.Service
{
    [Authenticate]
    public class AddDepartmentService : ServiceBase
    {
        public override string URL => "/api/Department/Add";
        public override HttpMethod HttpMethod => HttpMethod.Post;
        private readonly IDepartmentRepository _departmentRepository;

        public AddDepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddDepartmentModel reqModel = CastToObject<ReqAddDepartmentModel>(requestModel);

            await _departmentRepository.AddDepartment(reqModel);

            return new ResponseModel<T>()
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.DepartmentID
                }
            };
        }

        public Task<ResponseModel<T>> ProcessDepartment<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

        protected override bool HandleError(IRequestModel requestModel, Exception ex)
        {
            if (ex is PermDuplicateRecordException recordException)
                if (recordException.IndexName == "IX_Department_DepartmentName")
                    throw new PermBusinessException("0008", recordException.Value);

            return true;
        }
    }
}
