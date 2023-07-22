using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Perm.Admin.Permission.Data.Repository.Abstraction;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.Model.Admin;

namespace Perm.Admin.Permission.Data.Repository
{
    public class PermissionRepository : Repository<PermissionModel>, IPermissionRepository
    {
        private readonly IPermissionDetailRepository _permissionDetailRepository;

        public PermissionRepository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor, IPermissionDetailRepository permissionDetailRepository) : base(dataContext, httpContextAccessor)
        {
            _permissionDetailRepository = permissionDetailRepository;
        }

        protected override IIncludableQueryable<PermissionModel, object> IncludeForeignKeys(IQueryable<PermissionModel> entities)
        {
            return entities.Include(s => s.PermissionDetail).ThenInclude(s => s.PageOption);
        }

        public async Task AddPermission(PermissionModel permission)
        {
            if (permission.PermissionDetail != null && permission.PermissionDetail.Count != 0)
            {
                foreach (PermissionDetailModel item in permission.PermissionDetail)
                {
                    item.PermissionID = 0;
                    item.PermissionDetailID = 0;
                    item.Permission = null;
                    item.ParamAccessType = null;
                    item.PageOption = null;
                }

                permission.PermissionID = 0;

                await AddAsync(permission);
                await Context.CommitChangesAsync();
            }
        }

        public async Task UpdatePermission(PermissionModel permission)
        {
            if (permission.PermissionDetail != null && permission.PermissionDetail.Count != 0)
            {
                Update(permission);

                await AddDeletePermissionDetail(permission);

                await Context.CommitChangesAsync();
            }
        }
        public async Task DeletePermission(long permissionID)
        {
            IQueryable<PermissionDetailModel> permissionDetails = _permissionDetailRepository.Query(s => s.PermissionID == permissionID);

            _permissionDetailRepository.DeleteRange(permissionDetails);

            Delete(new PermissionModel { PermissionID = permissionID });

            await Context.CommitChangesAsync();
        }

        private async Task AddDeletePermissionDetail(PermissionModel reqPermissionModel)
        {
            PermissionModel dbPermission = Query(s => s.PermissionID == reqPermissionModel.PermissionID).FirstOrDefault();
            List<PermissionDetailModel> deletePermissionDetails = new List<PermissionDetailModel>();

            if (dbPermission.PermissionDetail != null)
            {
                foreach (PermissionDetailModel dbPermissionDetail in dbPermission.PermissionDetail)
                {
                    if (reqPermissionModel.PermissionDetail is not null && !reqPermissionModel.PermissionDetail.Exists(s => s.PermissionDetailID == dbPermissionDetail.PermissionDetailID))
                    {
                        deletePermissionDetails.Add(new PermissionDetailModel { PermissionDetailID = dbPermissionDetail.PermissionDetailID });
                    }
                }
            }

            _permissionDetailRepository.DeleteRange(deletePermissionDetails);

            if (reqPermissionModel.PermissionDetail != null)
            {
                foreach (PermissionDetailModel reqPermissionDetail in reqPermissionModel.PermissionDetail)
                {
                    if (dbPermission.PermissionDetail != null && !dbPermission.PermissionDetail.Exists(s => s.PermissionDetailID == reqPermissionDetail.PermissionDetailID))
                    {
                        await _permissionDetailRepository.AddAsync(reqPermissionDetail);
                    }
                }
            }
        }
    }
}
