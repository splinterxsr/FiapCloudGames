using System.Text;
using System.Text.Json;
using Reqnroll;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Tests.Factory;
using Moq;
using FiapCloudGames.Api.Models;

namespace FiapCloudGames.Tests.Steps.Usuarios
{
    [Binding]
    public class AdicionarUsuarioStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");

        public AdicionarUsuarioStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _usuarioRepositoryMock = factory.UsuarioRepositoryMock;
        }

        [Given("que eu preencho os dados do novo usuário:")]
        [Given("que eu tento adicionar um usuário com os seguintes dados:")]
        public void GivenPreencherDadosUsuario(DataTable dataTable)
        {
            var row = dataTable.Rows[0];
            var email = row["Email"];

            var request = new UsuarioInsertRequest
            {
                Nome = row.ContainsKey("Nome") ? row["Nome"] : string.Empty,
                Email = email,
                Senha = row["Senha"],
                PerfilId = (row["PerfilId"] == "null") ? null : int.Parse(row["PerfilId"])
            };

            _usuarioRepositoryMock
                .Setup(x => x.ObterAsync(It.Is<string>(s => s == email), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Usuario)null);

            _scenarioContext["UsuarioRequest"] = request;
        }

        [Given("que já existe um usuário cadastrado com o e-mail {string}")]
        public void GivenQueJaExisteUmUsuarioCadastradoComOEmail(string email)
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Usuario("Usuário Teste", email, "Senha@123", 1));
        }

        [Given("que eu preencho os dados do novo usuário com o e-mail {string}")]
        public void GivenQueEuPreenchoOsDadosDoNovoUsuarioComOEmail(string email)
        {
            var request = new UsuarioInsertRequest
            {
                Nome = "Usuário Teste",
                Email = email,
                Senha = "Senha@123",
                PerfilId = 1
            };

            _scenarioContext["UsuarioRequest"] = request;
        }

        [When("eu solicitar a adição do usuário")]
        public async Task WhenEuSolicitarAAdicaoDoUsuario()
        {
            var request = _scenarioContext.Get<UsuarioInsertRequest>("UsuarioRequest");
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/usuario/Adicionar", content);

            _scenarioContext["Response"] = response;
        }

        [Then("a resposta deve conter a mensagem de erro {string}")]
        public async Task ThenARespostaDeveConterAMensagemDeErro(string mensagemEsperada)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("Response");
            var corpoResposta = await response.Content.ReadAsStringAsync();

            Assert.Contains(mensagemEsperada, corpoResposta);
        }

        [BeforeScenario]
        public void ClearMocks()
        {
            _usuarioRepositoryMock.Invocations.Clear();
            _usuarioRepositoryMock.Reset();
        }
    }
}