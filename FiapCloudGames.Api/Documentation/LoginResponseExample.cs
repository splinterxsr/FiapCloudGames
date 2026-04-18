using FiapCloudGames.Api.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FiapCloudGames.Api.Documentation
{
    public class LoginResponseExample : IExamplesProvider<LoginResponse>
    {
        public LoginResponse GetExamples()
        {
            return new LoginResponse
            {
                Email = "usuario@example.com",
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c3VhcmlvQGV4YW1wbGUuY29tIiwibmFtZSI6Ikpvw6NvIFNpbHZhIiwicm9sZSI6IkFkbWluIn0.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ"
            };
        }
    }
}