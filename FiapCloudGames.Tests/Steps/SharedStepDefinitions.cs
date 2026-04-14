using FiapCloudGames.Tests.Factory;
using Reqnroll;
using System.Net.Http.Headers;

namespace FiapCloudGames.Tests
{
    [Binding]
    public class SharedStepDefinitions
    {

        private readonly HttpClient _client;
        private readonly ScenarioContext _scenarioContext;

        public SharedStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
        {
            _client = factory.CreateClient();
            _scenarioContext = scenarioContext;
            _scenarioContext["HttpClient"] = _client;
        }

        [Given("que eu estou autenticado como {string}")]
        public void GivenQueEuEstouAutenticadoComo(string perfil)
        {
            var perfilId = perfil == "Administrador" ? "1" : "2";

            var token = TokenGenerator.GenerateToken(perfilId);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [Then(@"o sistema deve retornar o status (\d+).*")]
        public void ThenValidarStatus(int statusCode)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("Response");
            Assert.Equal(statusCode, (int)response.StatusCode);
        }
    }
}