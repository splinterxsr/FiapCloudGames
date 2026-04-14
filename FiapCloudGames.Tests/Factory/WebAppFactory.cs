using FiapCloudGames.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace FiapCloudGames.Tests.Factory
{
    public class WebAppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<IUsuarioRepository> UsuarioRepositoryMock { get; } = new();
        public Mock<IJogoRepository> JogoRepositoryMock { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IUsuarioRepository>();
                services.RemoveAll<IUsuarioRepository>();

                services.AddSingleton<IUsuarioRepository>(UsuarioRepositoryMock.Object);
                services.AddSingleton<IJogoRepository>(JogoRepositoryMock.Object);
            });
        }
    }
}