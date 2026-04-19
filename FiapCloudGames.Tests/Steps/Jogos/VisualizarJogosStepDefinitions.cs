using Reqnroll;
using Moq;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Tests.Factory;
using FiapCloudGames.Domain.Repositories;
using System.Text.RegularExpressions;

namespace FiapCloudGames.Tests
{
    namespace FiapCloudGames.Tests
    {
        [Binding]
        public class VisualizarJogosStepDefinitions
        {
            private readonly Mock<IJogoRepository> _jogoRepositoryMock;
            private readonly ScenarioContext _scenarioContext;
            private HttpResponseMessage _response;

            private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");

            public VisualizarJogosStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
            {
                _jogoRepositoryMock = factory.JogoRepositoryMock;
                _scenarioContext = scenarioContext;
            }

            [Given("que existe um jogo cadastrado com nome {string} e ID {int}")]
            public void GivenQueExisteUmJogoCadastradoComIDENome(string nome, int id)
            {
                var jogoMock = new Jogo(id, nome);

                _jogoRepositoryMock
                    .Setup(r => r.ObterAsync(id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(jogoMock);

                _jogoRepositoryMock
                    .Setup(r => r.ObterAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync([jogoMock, jogoMock, jogoMock]);
            }

            [When(@"eu solicitar (a lista de todos os jogos|os detalhes do jogo .*)")]
            public async Task WhenEuSolicitar(string acao)
            {
                var acaoIncluiId = Regex.Match(acao, @"\d+");

                if (acaoIncluiId.Success) _response = await Client.GetAsync($"/jogo/ObterPorId/{acaoIncluiId.Value}");
                else _response = await Client.GetAsync("/jogo/ObterTodos");

                _scenarioContext["Response"] = _response;
            }
        }
    }
}
