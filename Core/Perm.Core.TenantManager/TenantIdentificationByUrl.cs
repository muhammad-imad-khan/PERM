using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Perm.Core.ExceptionManager;
using Perm.Core.TenantManager.Abstraction;

namespace Perm.Core.TenantManager
{
    public class TenantIdentificationByUrl : ITenantIdentificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<List<TenantConfigModel>> _tenantConfigs;

        public TenantIdentificationByUrl(IOptions<List<TenantConfigModel>> tenantConfigs,
            IHttpContextAccessor httpContextAccessor)
        {
            _tenantConfigs = tenantConfigs;
            _httpContextAccessor = httpContextAccessor;
        }

        public ITenantConfigModel GetCurrentTenant()
        {
            if (_httpContextAccessor.HttpContext.Request.Headers["ClientURL"].Count > 0)
            {
                string clientURL = _httpContextAccessor.HttpContext.Request.Headers["ClientURL"][0];

                ITenantConfigModel tenantConfigModel =
                    _tenantConfigs.Value.FirstOrDefault(s => clientURL.ToLower().Contains(s.TenantID.ToLower()));

                if (tenantConfigModel == null)
                    throw new PermArgumentException($"Tenant configuration not found for URL: {clientURL}", null);
                return tenantConfigModel;
            }
            throw new PermArgumentException("ClientURL header is required.", null);
        }

        public ITenantConfigModel GetCurrentTenantByID(string tenantID)
        {
            ITenantConfigModel tenantConfigModel =
                _tenantConfigs.Value.FirstOrDefault(s => tenantID.ToLower().Equals(s.TenantID.ToLower()));

            return tenantConfigModel;
        }
    }
}