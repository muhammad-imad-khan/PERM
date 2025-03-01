using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Perm.Common.APIHelper.Default;
using Perm.Core.ComponentFactoryManager;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.TenantManager;
using Perm.Core.TenantManager.Abstraction;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.Database.SqlServer.Interceptor;
using Perm.Security.AuthenticateManager;

namespace Perm.Common.APIHelper
{
    public static class ApiStartup
    {
        /// <summary>
        ///     Entry method of API
        /// </summary>
        /// <param name="args"></param>
        public static IHostBuilder InitializeMainMethod(string[] args)
        {
            return CreateHostBuilder(args);
        }

        public static IServiceCollection PermServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddEFSecondLevelCache(options =>
            //{
            //    options.UseMemoryCacheProvider(CacheExpirationMode.Absolute, TimeSpan.FromDays(1))
            //        .DisableLogging()
            //        .UseMemoryCacheProvider()
            //        .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromDays(1));
            //});

            services.AddScoped<SessionContextInterceptor>();
            services.AddScoped<CommandInterceptor>();

            services.AddDbContext<PermDataContext>((serviceProvider, options) =>
                options
                    .UseSqlServer("ppp")
                    .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>())
                    .AddInterceptors(serviceProvider.GetRequiredService<SessionContextInterceptor>())
                    .EnableSensitiveDataLogging()
            );

            services.AddCors(option => option.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));

            services.AddResponseCaching();

            services.AddMvc(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
            });

            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddHttpContextAccessor();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            services.Configure<List<TenantConfigModel>>(configuration.GetSection("TenantConfig"));

            services.AddTransient<IComponentFactory, ComponentFactory>();
            services.AddScoped<ITenantIdentificationService, TenantIdentificationByLogin>();
            services.AddScoped<Authenticate>();

            services.LoadDependencies();
            services.AddScoped(typeof(ServiceBase), typeof(DefaultService));

            Constant.ServiceDescriptors = services.ToList();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            return services;
        }

        public static IApplicationBuilder PermAppConfigure(this IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IEnumerable<ServiceBase> componentBase)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseResponseCaching();

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapRestApi(services, componentBase.ToList());

            return app;
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_,
                    configurationBuilder) =>
                {
                    configurationBuilder.SetBasePath($@"{AppDomain.CurrentDomain.BaseDirectory}");
                    configurationBuilder.AddJsonFile("appsettings.json", false, true);
                    configurationBuilder.AddJsonFile("TenantConfig.json", false, true);
                    configurationBuilder.AddEnvironmentVariables();
                });
        }
    }
}