using Microsoft.Extensions.DependencyInjection;

namespace Perm.Core.DependencyResolver
{
    public class DependencyRegister : IDependencyRegister
    {
        private readonly IServiceCollection _serviceCollection;

        public DependencyRegister(IServiceCollection serviceCollection)
        {
            this._serviceCollection = serviceCollection;
        }

        void IDependencyRegister.AddScoped<TService>()
        {
            _serviceCollection.AddScoped<TService>();
        }

        void IDependencyRegister.AddScoped<TService, TImplementation>()
        {
            _serviceCollection.AddScoped<TService, TImplementation>();
        }

        void IDependencyRegister.AddScopedForMultiImplementation<TService, TImplementation>()
        {
            _serviceCollection.AddScoped<TImplementation>()
                .AddScoped<TService, TImplementation>(s => s.GetService<TImplementation>());
        }

        void IDependencyRegister.AddSingleton<TService>()
        {
            _serviceCollection.AddSingleton<TService>();
        }

        void IDependencyRegister.AddSingleton<TService, TImplementation>()
        {
            _serviceCollection.AddSingleton<TService, TImplementation>();
        }

        void IDependencyRegister.AddTransient<TService>()
        {
            _serviceCollection.AddTransient<TService>();
        }

        void IDependencyRegister.AddTransient<TService, TImplementation>()
        {
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        void IDependencyRegister.AddTransientForMultiImplementation<TService, TImplementation>()
        {
            _serviceCollection.AddTransient<TImplementation>()
                .AddTransient<TService, TImplementation>(s => s.GetService<TImplementation>());
        }
    }
}