using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Infra.Data.Services;
using FiapCloudGames.Tests.Factory;
using Microsoft.AspNetCore.Http.Timeouts;
using Moq;
using Reqnroll;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace FiapCloudGames.Tests
{
    [Binding]
    public class SharedStepDefinitions
    {

        private readonly HttpClient _client;
        private readonly ScenarioContext _scenarioContext;
        private readonly SenhaService _senhaService = new();
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IJogoRepository> _jogoRepositoryMock;

        public SharedStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
        {
            _client = factory.CreateClient();
            _scenarioContext = scenarioContext;
            _scenarioContext["HttpClient"] = _client;
            _usuarioRepositoryMock = factory.UsuarioRepositoryMock;
            _jogoRepositoryMock = factory.JogoRepositoryMock;
        }

        //Método genérico para autenticar com o perfil informado e adicionar o token JWT no HttpClient para as requisições futuras
        [Given("que eu estou autenticado como {string}")]
        public void GivenQueEuEstouAutenticadoComo(string perfil)
        {
            var perfilId = perfil == "Administrador" ? "1" : "2";

            var token = TokenGenerator.GenerateToken(perfilId);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        //Método genérico para definir que determinada entidade existe no contexto do teste
        [Given(@"^(?:que )?existe um (jogo|usuário) cadastrado com ID (.*)$")]
        public void GivenQueExisteEntidadeCadastradaComID(string entidade, int id)
        {
            if (entidade.ToLower().Contains("usuário"))
            {
                var usuario = new Usuario(id, "Usuário Teste", "email@teste.com", _senhaService.CriaHash("Senha@Teste123"), 1);
                _usuarioRepositoryMock.Setup(r => r.ObterAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(usuario);
            }
            else
            {
                var jogo = new Jogo(id, "Jogo Base Teste");
                _jogoRepositoryMock.Setup(r => r.ObterAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(jogo);
            }
        }

        //Método genérico para definir que determinada entidade não existe no contexto do teste 
        [Given(@"^(?:que )?não existe um (jogo|usuário) cadastrado com ID (.*)$")]
        public void GivenQueNaoExisteEntidadeComID(string entidade, int id)
        {
            if (entidade.ToLower().Contains("usuário")) _usuarioRepositoryMock.Setup(r => r.ObterAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Usuario)null);
            else _jogoRepositoryMock.Setup(r => r.ObterAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Jogo)null);
        }

        //Método genérico para validação do status code retornado pela resposta da requisição na API
        [Then(@"o sistema deve retornar o status (\d+).*")]
        public void ThenValidarStatus(int statusCode)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("Response");
            Assert.Equal(statusCode, (int)response.StatusCode);
        }
    }
}