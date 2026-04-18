using FiapCloudGames.Api.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FiapCloudGames.Api.Documentation
{
    public class LoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            return new LoginRequest
            {
                Email = "usuario@example.com",
                Senha = "Senha123!@#"
            };
        }
    }
}