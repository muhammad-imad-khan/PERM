using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Abstraction;
using Perm.Model.Abstraction.Enum;

namespace Perm.DataAccessLayer.DataRepository.Core.CustomField
{
    public class CustomFieldRepository : Repository<CustomFieldModel>, ICustomFieldRepository
    {
        public CustomFieldRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }

        /// <inheritdoc />
        public async Task<CustomFieldModel> GetByEntityIDAsync(EnumEntityType entityType, long entityID)
        {
            CustomFieldModel paginationQueryable =
                await QueryWithPagination(s => s.EntityID == entityID && s.ParamEntityTypeID == (int)entityType)
                .OriginalQueryable
                .FirstOrDefaultAsync();

            return paginationQueryable;
        }

        /// <inheritdoc />
        public IQueryable<CustomFieldModel> GetByEntityTypeIDAsync(EnumEntityType entityType)
        {
            return Query(s => s.ParamEntityTypeID == (long)entityType && s.IsDeleted == false);
        }

        /// <inheritdoc />
        public async Task SaveAsync(CustomFieldModel entity, long entityID, EnumEntityType entityType)
        {
            if (entity != null)
            {
                CustomFieldModel customField = await GetByEntityIDAsync(entityType, entityID);
                if (customField == null)
                {
                    entity.CustomFieldID = 0;
                    entity.EntityID = entityID;
                    await AddAsync(entity);
                }
                else
                {
                    entity.CustomFieldID = customField.CustomFieldID;
                    entity.EntityID = entityID;
                    Update(entity);
                }
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(long entityID, EnumEntityType entityType)
        {
            CustomFieldModel customField = await GetByEntityIDAsync(entityType, entityID);
            if (customField != null)
            {
                Delete(customField);
            }
        }
    }
}