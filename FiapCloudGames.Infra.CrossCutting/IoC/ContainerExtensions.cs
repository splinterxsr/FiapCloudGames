using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Domain.Services;
using FiapCloudGames.Infra.CrossCutting.Security;
using FiapCloudGames.Infra.Data.Contexts;
using FiapCloudGames.Infra.Data.Repositories;
using FiapCloudGames.Infra.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FiapCloudGames.Infra.CrossCutting.IoC
{
    public static class ContainerExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<MySqlContext>();

            services.AddTransient<ICorrelationIdService, CorrelationIdService>();
            services.AddTransient<JwtService>();
            services.AddTransient<UsuarioService>();
            services.AddTransient<ISenhaService, SenhaService>();

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IJogoRepository, JogoRepository>();

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            return services;
        }
    }
}