using Microsoft.AspNetCore.Http;
using Perm.Common;
using Perm.Core.ComponentFactoryManager;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Security.AuthenticateManager;

namespace Perm.Core.ModuleFactoryManager
{
    public class ComponentFactory : IComponentFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnumerable<ServiceBase> _moduleBase;
        private readonly PermDataContext _permDataContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly Authenticate _authenticate;

        public ComponentFactory(IHttpContextAccessor httpContextAccessor, IEnumerable<ServiceBase> moduleBase, PermDataContext permDataContext, IServiceProvider serviceProvider, Authenticate authenticate)
        {
            _httpContextAccessor = httpContextAccessor;
            _moduleBase = moduleBase;
            _permDataContext = permDataContext;
            _serviceProvider = serviceProvider;
            _authenticate = authenticate;
        }

        public ServiceBase GetComponent()
        {
            return GetComponent(_httpContextAccessor.HttpContext.Items["RequestUrl"].ToString());
        }

        public ServiceBase GetComponent(string url)
        {
            foreach (ServiceBase serviceBase in _moduleBase)
            {
                if (url.SameAs(serviceBase.URL))
                {
                    serviceBase.ServiceProvider = _serviceProvider;
                    return serviceBase;
                }
            }

            if (url == "/")
            {

            }
            throw new PermArgumentException($"No service registered for URL:{url}", null);
        }

        public void IsAuthorized(long userID, string apiEndPoint, string httpHeader)
        {
            try
            {
                bool isAuthorize = _permDataContext.ScalarFunction<bool>("Admin.Func_IsPermissionAllow",
                    new
                    {
                        userID,
                        apiEndPoint,
                        httpHeader
                    });

                if (!isAuthorize)
                    throw new UnauthorizedAccessException();
            }
            catch
            {
                // ignored
            }
        }

        public void IsAuthenticate(ServiceBase serviceBase)
        {
            object customAttributes = serviceBase.GetType().GetCustomAttributes(typeof(AuthenticateAttribute), false).FirstOrDefault();
            if (customAttributes is AuthenticateAttribute)
            {
                _authenticate.Validate();
            }
        }
    }
}