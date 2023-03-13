using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Perm.Common;
using Perm.Common.APIModel;
using Perm.Core.ComponentFactoryManager;
using Perm.Core.RequestManager.Processor;

namespace Perm.Core.RequestManager;

internal static class Main
{
    internal static async Task<ResponseModel<object>> RequestDelegateAsync(IServiceProvider serviceProvider, HttpContext context)
    {
        IServiceScope serviceScopes = serviceProvider.CreateScope();
        IServiceProvider provider = serviceScopes.ServiceProvider;

        IComponentFactory requiredService = provider.GetRequiredService<IComponentFactory>();
        ServiceBase serviceBase = requiredService.GetComponent();

        ResponseModel<object> processRequestAsync = await RequestHandler.ProcessRequestWithExceptions(async ()
                => await ExecuteRequestAsync(context, provider)
            , provider, serviceBase);

        return processRequestAsync;
    }

    private static async Task<ResponseModel<object>> ExecuteRequestAsync(HttpContext context, IServiceProvider provider)
    {
        IComponentFactory requiredService = provider.GetRequiredService<IComponentFactory>();
        ServiceBase serviceBase = requiredService.GetComponent();

        requiredService.IsAuthenticate(serviceBase);
        requiredService.IsAuthorized(context.Items["UserID"].ParseTo<long>(), serviceBase.URL, string.Join(',', context.Request.Headers.Select(s => s.Key)));

        string body;
        if (context.Items["Body"] is null)
        {
            var stream = new StreamReader(context.Request.Body);
            body = await stream.ReadToEndAsync();
        }
        else
        {
            body = context.Items["Body"].ToString();
        }

        ResponseModel<object> processRequestAsync = await serviceBase.ProcessRequestAsync<object>(new RequestModelBase { RequestBodyRaw = body });

        processRequestAsync.IsStaticContent = serviceBase.GetType().GetCustomAttributes(typeof(StaticContentAttribute), false).Length != 0;

        return processRequestAsync;
    }
}