using Perm.Common.APIHelper;
using Perm.Core.RequestManager.Processor;

namespace Perm.RestAPI
{
    /// <summary>
    /// This class configure Perm API services
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This class configure Perm API services
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.PermServiceCollection(_configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="services"></param>
        /// <param name="componentBase"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IEnumerable<ServiceBase> componentBase)
        {
            app.PermAppConfigure(env, services, componentBase);
        }
    }
}