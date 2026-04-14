using Reqnroll;
using System.Net.Http.Headers;
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
            private readonly HttpClient _client;
            private readonly Mock<IJogoRepository> _jogoRepositoryMock;
            private readonly ScenarioContext _scenarioContext;
            private HttpResponseMessage _response;

            public VisualizarJogosStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
            {
                _jogoRepositoryMock = factory.JogoRepositoryMock;
                _client = factory.CreateClient();
                _scenarioContext = scenarioContext;
            }

            [Given("que eu estou autenticado")]
            public void GivenQueEuEstouAutenticado()
            {
                var token = TokenGenerator.GenerateToken("1");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            [Given("que existe um jogo cadastrado com ID {int} e nome {string}")]
            public void GivenQueExisteUmJogoCadastradoComIDENome(int id, string nome)
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

                if (acaoIncluiId.Success) _response = await _client.GetAsync($"/jogo/ObterPorId/{acaoIncluiId.Value}");
                else _response = await _client.GetAsync("/jogo/ObterTodos");

                _scenarioContext["Response"] = _response;
            }
        }
    }
}
