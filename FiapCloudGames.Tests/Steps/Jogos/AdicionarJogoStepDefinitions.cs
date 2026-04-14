using System.Text;
using System.Text.Json;
using Reqnroll;
using FiapCloudGames.Tests.Factory;
using FiapCloudGames.Api.Models;
using FiapCloudGames.Domain.Repositories;
using Moq;
using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Tests.Steps.Jogos
{
    [Binding]
    public class AdicionarJogoStepDefinitions
    {
        private readonly Mock<IJogoRepository> _jogoRepositoryMock;
        private readonly ScenarioContext _scenarioContext;
        private HttpResponseMessage _response;


        private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");
        public AdicionarJogoStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
        {
            _jogoRepositoryMock = factory.JogoRepositoryMock;
            _scenarioContext = scenarioContext;
        }

        [Given("que eu preencho os dados do jogo:")]
        [Given("que eu tento adicionar um jogo com os seguintes dados:")]
        public void GivenPreencherDadosJogo(DataTable dataTable)
        {
            var nomeJogo = dataTable.Rows[0]["Nome"];

            var request = new JogoInsertRequest { Nome = nomeJogo };

            _jogoRepositoryMock
                .Setup(x => x.AdicionarAsync(It.IsAny<Jogo>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _scenarioContext["JogoRequest"] = request;
        }

        [When("eu solicitar a adição do jogo")]
        public async Task WhenEuSolicitarAAdicaoDoJogo()
        { 
            var request = _scenarioContext.Get<JogoInsertRequest>("JogoRequest");
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            _response = await Client.PostAsync("/jogo/Adicionar", content);

            _scenarioContext["Response"] = _response;
        }

        [Then("a mensagem de erro deve ser {string}")]
        public async Task ThenAMensagemDeErroDeveSer(string mensagemEsperada)
        {
            var response = await _response.Content.ReadAsStringAsync();

            Assert.Contains(mensagemEsperada, response);
        }

        [BeforeScenario]
        public void ClearMocks()
        {
            _jogoRepositoryMock.Invocations.Clear();
            _jogoRepositoryMock.Reset();
        }
    }
}