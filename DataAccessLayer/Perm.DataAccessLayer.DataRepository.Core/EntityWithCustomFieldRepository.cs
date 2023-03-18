using Microsoft.AspNetCore.Http;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Abstraction;
using Perm.Model.Abstraction.Enum;

namespace Perm.DataAccessLayer.DataRepository.Core
{
    public class EntityWithCustomFieldRepository<T> : Repository<T>, IEntityWithCustomFieldRepository<T> where T : CustomFieldModelBase
    {
        protected EntityWithCustomFieldRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
        }

        public Task AddWithCustomFieldAsync(T entity, Func<T, long> entityID, EnumEntityType entityType)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWithCustomFieldAsync(T entity, long getProp, EnumEntityType entityType)
        {
            throw new NotImplementedException();
        }

        public Task<EntityWithCustomFieldList<T>> GetAllWithCustomFieldAsync(EnumEntityType entityType, Func<T, long> getProp)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWithCustomFieldAsync(T entity, long entityID, EnumEntityType entityType)
        {
            throw new NotImplementedException();
        }
    }
}