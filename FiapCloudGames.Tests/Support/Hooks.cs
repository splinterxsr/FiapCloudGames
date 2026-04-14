using FiapCloudGames.Tests.Factory;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace FiapCloudGames.Tests.Support
{
    [Binding]
    public class Hooks
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<WebAppFactory<Program>>();

            return services;
        }
    }
}