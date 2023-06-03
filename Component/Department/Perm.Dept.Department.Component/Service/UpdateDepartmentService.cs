using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.Dept.Department.Data.Repository;
using Perm.Dept.Department.Module.APIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Dept.Department.Module.Service
{
    [Authenticate]
    public class UpdateDepartmentService : ServiceBase
    {
        public override string URL => "/api/Department/Update";
        public override HttpMethod HttpMethod => HttpMethod.Put;
        private readonly IDepartmentRepository _departmentRepository;

        public UpdateDepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddDepartmentModel reqModel = CastToObject<ReqAddDepartmentModel>(requestModel);

            await _departmentRepository.UpdateDepartment(reqModel);

            return new ResponseModel<T>();
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
