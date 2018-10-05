using Nomad.DotNet.API;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNomadApi(
            this IServiceCollection services)
        {
            services.AddScoped<AclPolicies>();
            services.AddScoped<AclTokens>();
            services.AddScoped<Agent>();
            services.AddScoped<Allocations>();
            services.AddScoped<Client>();
            services.AddScoped<Deployments>();
            services.AddScoped<Evaluations>();
            services.AddScoped<Jobs>();
            services.AddScoped<Namespaces>();
            services.AddScoped<Nodes>();
            services.AddScoped<Quotas>();
            services.AddScoped<Regions>();
            services.AddScoped<Search>();
            services.AddScoped<SentinelPolicies>();
            services.AddScoped<Status>();
            services.AddScoped<SystemMaintenance>();
            services.AddScoped<Validate>();

            return services;
        }
    }
}