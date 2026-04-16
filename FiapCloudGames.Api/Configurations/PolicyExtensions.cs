using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Api.Configurations
{
    public static class PolicyExtensions
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(nameof(Policy.Administrador), policy => policy.RequireRole(nameof(Policy.Administrador)))
                .AddPolicy(nameof(Policy.Usuario), policy => policy.RequireRole(nameof(Policy.Usuario)))
                .AddPolicy(nameof(Policy.Todos), policy => policy.RequireRole(nameof(Policy.Administrador), nameof(Policy.Usuario)));

            return services;
        }
    }
}