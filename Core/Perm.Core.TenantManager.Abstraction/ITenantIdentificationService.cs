namespace Perm.Core.TenantManager.Abstraction
{
    public interface ITenantIdentificationService
    {
        public ITenantConfigModel GetCurrentTenant();
        public ITenantConfigModel GetCurrentTenantByID(string tenantID);
    }
}