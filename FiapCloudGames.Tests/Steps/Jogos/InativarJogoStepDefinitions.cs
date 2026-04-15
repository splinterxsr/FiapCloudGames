using Moq;
using Reqnroll;

namespace FiapCloudGames.Tests.Steps.Jogos
{
    [Binding]
    public class InativarJogoStepDefinitions
    {
        private HttpResponseMessage _response;
        private readonly ScenarioContext _scenarioContext;

        private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");

        public InativarJogoStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("eu solicitar a inativação do jogo {int}")]
        public async Task WhenEuSolicitarAInativacaoDoJogoAsync(int id)
        {
            _response = await Client.PostAsync($"/jogo/inativar/{id}", null, It.IsAny<CancellationToken>());

            _scenarioContext["Response"] = _response;
        }
    }
}
