using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Setup;
using Perm.Setup.Common.Data.Repository.Abstraction;

namespace Perm.Setup.Common.Data.Repository
{
    /// <inheritdoc cref="PageOptionRepository" />
    public class PageOptionRepository : Repository<PageOptionModel>, IPageOptionRepository
    {
        /// <inheritdoc/>
        public PageOptionRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }
    }
}