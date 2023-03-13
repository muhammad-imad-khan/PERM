using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using Perm.Admin.Audit.Component.APIModel;
using Perm.Admin.AuditTrail.Data.Repository;
using Perm.Admin.AuditTrail.Data;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Common;
using Perm.DataAccessLayer.DataRepository.Core;

namespace Perm.Admin.AuditTrail.Module.Service;

[Authenticate]
public class GetAuditTrailService : ServiceBase
{
    public override string URL => "/api/AuditTrail";
    public override HttpMethod HttpMethod => HttpMethod.Get;
    private readonly IAuditTrailRepository _auditTrailRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PermDataContext _dataContext;

    public GetAuditTrailService(IAuditTrailRepository auditTrailRepository, IHttpContextAccessor httpContextAccessor, PermDataContext dataContext)
    {
        _auditTrailRepository = auditTrailRepository;
        _httpContextAccessor = httpContextAccessor;
        _dataContext = dataContext;
    }

    private List<PropertyInfo> GetPropertyInfo(Type clrType)
    {
        List<PropertyInfo> properties = clrType.GetProperties().ToList();
        if (clrType?.BaseType?.BaseType is not null)
        {
            List<PropertyInfo> propertyInfos = GetPropertyInfo(clrType.BaseType);
            properties.AddRange(propertyInfos);
        }

        return properties;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        string tableName = _httpContextAccessor.HttpContext.Request.Headers["tableName"].ToString().ToUpper();
        long recordID = _httpContextAccessor.HttpContext.Request.Headers["recordID"].ParseTo<long>();

        IQueryable<AuditTrailMasterModel> auditTrails = _auditTrailRepository.GetAll()
            .Where(c => c.EntityType.ToUpper() == tableName && c.RecordID == recordID)
            .GroupBy(cg => new
            {
                cg.ChangeOn,
                cg.ChangeByName
            })
            .Select(cs => new AuditTrailMasterModel
            {
                ChangeOn = cs.Key.ChangeOn,
                ChangeByName = cs.Key.ChangeByName,
                Detail = cs.ToList(),
                EventID = cs.Min(c => c.EventID) != cs.Max(c => c.EventID) ? 2 : cs.Min(c => c.EventID)
            });

        var (pageNo, rowCount, _, _) = _auditTrailRepository.GetPaginationCriteriaFromHeader();

        PaginationQueryable<AuditTrailMasterModel> applyPaginationAndSorting = auditTrails.NotCacheable().ApplyPaginationAndSorting(pageNo, rowCount, "ChangeOn", false, false);
        PaginationList<AuditTrailMasterModel> auditTrail = await applyPaginationAndSorting.ToPaginationListAsync();

        List<AuditInfoModel> auditInfo = GetAuditInfo();

        RemoveNonAuditColumns(auditTrail, auditInfo);

        return new ResponseModel<T>
        {
            Data = (T)(object)auditTrail
        };
    }

    private static void RemoveNonAuditColumns(PaginationList<AuditTrailMasterModel> auditTrail, List<AuditInfoModel> auditInfo)
    {
        foreach (AuditTrailMasterModel auditTrailMaster in auditTrail.List)
        {
            IEnumerable<ViewAuditTrailModel> viewAuditTrailModels = auditTrailMaster.Detail.ToList();
            foreach (ViewAuditTrailModel viewAuditTrail in viewAuditTrailModels)
            {
                //Get audit info specif to current table
                AuditInfoModel tableAuditInfo = auditInfo.FirstOrDefault(c => c.TableName == viewAuditTrail.TableName && c.AuditProperties.Count != 0);

                AuditProperty auditProperty = tableAuditInfo?.AuditProperties.FirstOrDefault(a => a.PropertyName == viewAuditTrail.TableColumnName && a.AuditAttribute is not null);
                if (auditProperty != null)
                {
                    viewAuditTrail.AuditAttribute = auditProperty.AuditAttribute;
                }
            }

            auditTrailMaster.Detail = viewAuditTrailModels.Where(r => r.AuditAttribute is null || !r.AuditAttribute.Ignore);
        }
    }

    private List<AuditInfoModel> GetAuditInfo()
    {
        List<IEntityType> entityTypes = _dataContext.Model.GetEntityTypes().ToList();

        List<AuditInfoModel> auditInfo = entityTypes.GroupBy(c => new AuditInfoModel
        {
            TableName = c.GetTableName(),
            AuditProperties = GetPropertyInfo(c.ClrType).Select(auditAtt => new AuditProperty
            {
                PropertyName = auditAtt.Name,
                AuditAttribute = auditAtt.GetCustomAttribute<AuditAttribute>()
            })
                    .Where(audiAtt => audiAtt.AuditAttribute is not null).Distinct()
                    .ToList()
        }).Where(c => c.Key.TableName is not null)
            .Select(c => c.Key).ToList();
        return auditInfo;
    }
}

public class AuditProperty : IEquatable<AuditProperty>
{
    public string PropertyName { get; set; }
    public AuditAttribute AuditAttribute { get; set; }
    public override string ToString()
    {
        return PropertyName;
    }

    public bool Equals(AuditProperty other)
    {
        return PropertyName == other?.PropertyName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PropertyName);
    }
}

public class AuditInfoModel
{
    public string TableName { get; set; }
    public List<AuditProperty> AuditProperties { get; set; }

    public override string ToString()
    {
        return $"{TableName}.[{string.Join(',', AuditProperties)}]";
    }
}