using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Abstraction.Enum;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Model.Abstraction;

namespace Perm.DataAccessLayer.DataRepository.Core.CustomField
{
    public class CustomFieldMetaRepository : Repository<CustomFieldMetaModel>, ICustomFieldMetaRepository
    {
        /// <inheritdoc />
        public CustomFieldMetaRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor) { }

        /// <inheritdoc />
        protected override IIncludableQueryable<CustomFieldMetaModel, object> IncludeForeignKeys(IQueryable<CustomFieldMetaModel> entities)
        {
            return entities
                    .Include(s => s.ParamEntityType)
                    .Include(s => s.ParamDataType);
        }

        /// <inheritdoc />
        public IQueryable<CustomFieldMetaModel> GetMetaByEntityTypeAsync(EnumEntityType entityType)
        {
            return Query(s => s.ParamEntityTypeID == (int)entityType && s.IsDeleted == false);
        }

        /// <inheritdoc />
        public async Task AddCustomFieldMeta(CustomFieldMetaModel userDefinedFieldMetaModel)
        {
            await AddAsync(userDefinedFieldMetaModel);
            await Context.CommitChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateCustomFieldMeta(CustomFieldMetaModel userDefinedFieldMetaModel)
        {
            Update(userDefinedFieldMetaModel);
            await Context.CommitChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteCustomFieldMeta(long userDefinedFieldMetaModelID)
        {
            Delete(new CustomFieldMetaModel { CustomMetaID = userDefinedFieldMetaModelID });
            await Context.CommitChangesAsync();
        }
    }
}