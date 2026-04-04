using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Infra.Data.Contexts;
using FiapCloudGames.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FiapCloudGames.Infra.CrossCutting.IoC
{
    public static class ContainerExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<MySqlContext>();

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}