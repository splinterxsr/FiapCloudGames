using FiapCloudGames.Api.Models;
using Reqnroll;
using System.Text;
using System.Text.Json;

namespace FiapCloudGames.Tests.Steps.Jogos
{
    [Binding]
    public class EditarJogoStepDefinitions
    {
        private HttpResponseMessage _response;
        private readonly ScenarioContext _scenarioContext;

        private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");

        public EditarJogoStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("eu solicitar a edição do jogo {int} com o nome {string}")]
        public async Task WhenEuSolicitarAEdicaoDoJogoComONomeAsync(int id, string nome)
        {
            var request = new JogoUpdateRequest { Id = id, Nome = nome };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            _response = await Client.PostAsync("/jogo/Editar", content);

            _scenarioContext["Response"] = _response;
        }
    }
}
