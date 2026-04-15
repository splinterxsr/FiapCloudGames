using Moq;
using Reqnroll;

namespace FiapCloudGames.Tests.Steps.Usuarios
{
    [Binding]
    public class InativarUsuarioStepDefinitions
    {
        private HttpResponseMessage _response;
        private readonly ScenarioContext _scenarioContext;

        private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");

        public InativarUsuarioStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("eu solicitar a inativação do usuário {int}")]
        public async Task WhenEuSolicitarAInativacaoDoUsuarioAsync(int id)
        {
            _response = await Client.PostAsync($"/usuario/inativar/{id}", null, It.IsAny<CancellationToken>());

            _scenarioContext["Response"] = _response;
        }
    }
}
