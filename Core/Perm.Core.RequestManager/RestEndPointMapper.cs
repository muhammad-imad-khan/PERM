using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Perm.Common.APIModel;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;

namespace Perm.Core.RequestManager
{
    public static class RestEndpointMapper
    {
        public static void MapRestApi(this IApplicationBuilder app, IServiceProvider services, List<ServiceBase> componentBase)
        {
            DependencyLoader.RegisteredServices = componentBase;
            IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            app.UseEndpoints(endpoints =>
            {
                List<string> routePatterns = endpoints.DataSources.FirstOrDefault()?.Endpoints.Select(s => (s as RouteEndpoint)).Select(s => (s?.RoutePattern.RawText)).ToList();
                if (routePatterns != null)
                {
                    foreach (ServiceBase serviceBase in componentBase)
                    {
                        string endpoint = routePatterns.FirstOrDefault(s => $@"/{s}" == serviceBase.URL);
                        if (endpoint != null)
                            continue;

                        string fullName = string.Empty;
                        if (serviceBase.HttpMethod == null)
                        {
                            fullName = serviceBase.GetType().FullName?.Split(".").LastOrDefault() ?? "";
                        }

                        if (fullName.StartsWith("Add") || serviceBase.HttpMethod == HttpMethod.Post)
                            endpoints.MapPost(serviceBase.URL, GetRequestDelegate(provider));
                        else if (fullName.StartsWith("Update") || serviceBase.HttpMethod == HttpMethod.Put)
                            endpoints.MapPut(serviceBase.URL, GetRequestDelegate(provider));
                        else if (fullName.StartsWith("Delete") || serviceBase.HttpMethod == HttpMethod.Delete)
                            endpoints.MapDelete(serviceBase.URL, GetRequestDelegate(provider));
                        else if (fullName.StartsWith("Get") || serviceBase.HttpMethod == HttpMethod.Get)
                            endpoints.MapGet(serviceBase.URL, GetRequestDelegate(provider));
                        else
                            endpoints.MapGet("/", GetRequestDelegate(provider));
                    }
                }
            });
        }

        private static RequestDelegate GetRequestDelegate(IServiceProvider provider)
        {
            return async delegate (HttpContext context)
            {
                context.Items["RequestUrl"] = context.Request.Path.Value;
                ResponseModel<object> requestDelegateAsync = await Main.RequestDelegateAsync(provider, context);
                string serializeObject;

                if (!requestDelegateAsync.IsStaticContent)
                {
                    context.Response.ContentType = "application/json; charset=utf-8";
                    serializeObject = JsonConvert.SerializeObject(requestDelegateAsync, null, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else
                {
                    serializeObject = requestDelegateAsync.Message;
                }

                serializeObject ??= "Invalid Response";

                await context.Response.WriteAsync(serializeObject);
            };
        }
    }
}