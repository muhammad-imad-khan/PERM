using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using Perm.Admin.Audit.Component.APIModel;
using Perm.Admin.Audit.Data.Repository;
using Perm.Admin.Audit.Data;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Common;
using Perm.DataAccessLayer.DataRepository.Core;

namespace Perm.Admin.Audit.Component.Service;

[Authenticate]
public class GetAuditService : ServiceBase
{
    public override string URL => "/api/Audit";
    public override HttpMethod HttpMethod => HttpMethod.Get;
    private readonly IAuditRepository _auditRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PermDataContext _dataContext;

    public GetAuditService(IAuditRepository auditRepository, IHttpContextAccessor httpContextAccessor, PermDataContext dataContext)
    {
        _auditRepository = auditRepository;
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

        IQueryable<AuditMasterModel> auditQuerable = _auditRepository.GetAll()
            .Where(c => c.EntityType.ToUpper() == tableName && c.RecordID == recordID)
            .GroupBy(cg => new
            {
                cg.ChangeOn,
                cg.ChangeByName
            })
            .Select(cs => new AuditMasterModel
            {
                ChangeOn = cs.Key.ChangeOn,
                ChangeByName = cs.Key.ChangeByName,
                Detail = cs.ToList(),
                EventID = cs.Min(c => c.EventID) != cs.Max(c => c.EventID) ? 2 : cs.Min(c => c.EventID)
            });

        var (pageNo, rowCount, _, _) = _auditRepository.GetPaginationCriteriaFromHeader();

        PaginationQueryable<AuditMasterModel> applyPaginationAndSorting = auditQuerable.NotCacheable().ApplyPaginationAndSorting(pageNo, rowCount, "ChangeOn", false, false);
        PaginationList<AuditMasterModel> auditPaginationList = await applyPaginationAndSorting.ToPaginationListAsync();

        List<AuditInfoModel> auditInfo = GetAuditInfo();

        RemoveNonAuditColumns(auditPaginationList, auditInfo);

        return new ResponseModel<T>
        {
            Data = (T)(object)auditPaginationList
        };
    }

    private static void RemoveNonAuditColumns(PaginationList<AuditMasterModel> auditPaginationList, List<AuditInfoModel> auditInfo)
    {
        foreach (AuditMasterModel audit in auditPaginationList.List)
        {
            IEnumerable<ViewAuditModel> viewAuditModels = audit.Detail.ToList();
            foreach (ViewAuditModel viewAudit in viewAuditModels)
            {
                //Get audit info specif to current table
                AuditInfoModel tableAuditInfo = auditInfo.FirstOrDefault(c => c.TableName == viewAudit.TableName && c.AuditProperties.Count != 0);

                AuditProperty auditProperty = tableAuditInfo?.AuditProperties.FirstOrDefault(a => a.PropertyName == viewAudit.TableColumnName && a.AuditAttribute is not null);
                if (auditProperty != null)
                {
                    viewAudit.AuditAttribute = auditProperty.AuditAttribute;
                }
            }

            audit.Detail = viewAuditModels.Where(r => r.AuditAttribute is null || !r.AuditAttribute.Ignore);
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