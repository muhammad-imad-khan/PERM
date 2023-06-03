using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Dept.Department.Data.Repository
{
    public interface IDepartmentRepository : IRepository<DepartmentModel>
    {
        public Task AddDepartment(DepartmentModel departmentModel);
        public Task UpdateDepartment(DepartmentModel departmentModel);
        public Task DeleteDepartment(long departmentID);


    }
}
