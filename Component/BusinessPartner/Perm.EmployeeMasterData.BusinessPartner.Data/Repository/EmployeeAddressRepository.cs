using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;
using Perm.Model.EmployeeMasterData;

namespace Perm.EmployeeMasterData.BusinessPartner.Data.Repository
{
    public class EmployeeAddressRepository : Repository<EmployeeAddressModel>, IEmployeeAddressRepository
    {
        public EmployeeAddressRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }
    }
}
