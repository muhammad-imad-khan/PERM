using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;
using Perm.Model.Employee;

namespace Perm.EmployeeMasterData.BusinessPartner.Data.Repository
{
    /// <inheritdoc cref="FeedbackRepository"/>
    public class FeedbackRepository : Repository<FeedbackModel>, IFeedbackRepository
    {
        public FeedbackRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }

        public async Task AddFeedback(FeedbackModel model)
        {
            await AddAsync(model);
            await Context.CommitChangesAsync();
        }
    }
}