using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Department;

namespace Perm.Dept.Department.Data.Repository
{
    public class DepartmentRepository : Repository<DepartmentModel>, IDepartmentRepository
    {
        public DepartmentRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }

        public async Task AddDepartment(DepartmentModel departmentModel)
        {
            await AddAsync(departmentModel);
            await Context.CommitChangesAsync();
        }

        public async Task UpdateDepartment(DepartmentModel departmentModel)
        {
            Update(departmentModel);
            await Context.CommitChangesAsync();
        }

        public async Task DeleteDepartment(long departmentID)
        {
            Delete(new DepartmentModel { DepartmentID = departmentID });
            await Context.CommitChangesAsync();
        }

        
    }
}
