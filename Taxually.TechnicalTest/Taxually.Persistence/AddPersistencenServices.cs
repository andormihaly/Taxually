using Microsoft.Extensions.DependencyInjection;
using Taxually.Application.Persistence.TaxRequest;
using Taxually.Persistence.Builders;
using Taxually.Persistence.Managers;

namespace Taxually.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<ITaxRequestManager, TaxRequestManager>();

            services.AddTransient<IResolverBuilder, ResolverBuilder>();

            return services;
        }
    }
}
