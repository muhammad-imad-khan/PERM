using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.Dept.Department.Data;
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
    public class DeleteDepartmentService : ServiceBase
    {
        public override string URL => "/api/Department/Delete";
        public override HttpMethod HttpMethod => HttpMethod.Delete;
        private readonly IDepartmentRepository _departmentRepository;
        public DeleteDepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqDeleteDepartmentModel reqModel = CastToObject<ReqDeleteDepartmentModel>(requestModel);

            await _departmentRepository.DeleteDepartment(reqModel.DepartmentID);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

    }
}
