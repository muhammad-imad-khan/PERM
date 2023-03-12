using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Perm.Core.ExceptionManager;
using Perm.Core.TenantManager.Abstraction;

namespace Perm.Core.TenantManager
{
    public class TenantIdentificationByLogin : ITenantIdentificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<List<TenantConfigModel>> _tenantConfigs;

        public TenantIdentificationByLogin(IOptions<List<TenantConfigModel>> tenantConfigs,
            IHttpContextAccessor httpContextAccessor)
        {
            _tenantConfigs = tenantConfigs;
            _httpContextAccessor = httpContextAccessor;
        }

        public ITenantConfigModel GetCurrentTenant()
        {
            // check request headers for TenantID if not found then check current http context (current session) for TenantID
            if (_httpContextAccessor.HttpContext.Request.Headers["TenantID"].Count > 0 || !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Items["TenantID"]?.ToString()))
            {
                IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;

                //string tenantID = _httpContextAccessor.HttpContext.Request.Headers["TenantID"]?[0] ?? _httpContextAccessor.HttpContext.Items["TenantID"]?.ToString();

                string tenantID = headers.TryGetValue("TenantID", out StringValues tenant)
                    ? tenant[0]
                    : _httpContextAccessor.HttpContext.Items["TenantID"]?.ToString();

                ITenantConfigModel tenantConfigModel = GetCurrentTenantByID(tenantID);

                if (tenantConfigModel == null)
                    throw new PermArgumentException($"Tenant configuration not found for Tenant ID: {tenantID}", null);

                return tenantConfigModel;
            }

            throw new PermArgumentException("TenantID header is required.", null);
        }

        public ITenantConfigModel GetCurrentTenantByID(string tenantID)
        {
            ITenantConfigModel tenantConfigModel =
                _tenantConfigs.Value.FirstOrDefault(s => string.Equals(s.TenantID, tenantID, StringComparison.CurrentCultureIgnoreCase));

            return tenantConfigModel;
        }
    }
}