using FiapCloudGames.Api.Models;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Tests.Factory;
using Moq;
using Reqnroll;
using System.Text;
using System.Text.Json;

namespace FiapCloudGames.Tests.Steps.Usuarios
{
    [Binding]
    public class EditarUsuarioStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private HttpResponseMessage _response;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");

        public EditarUsuarioStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
        {
            _usuarioRepositoryMock = factory.UsuarioRepositoryMock;
            _scenarioContext = scenarioContext;
        }

        [Given("que eu preencho os dados para editar o usuário {int}:")]
        public void GivenQueEuPreenchoOsDadosParaEditarOUsuario(int id, DataTable dataTable)
        {
            var row = dataTable.Rows[0];

            var request = new UsuarioUpdateRequest
            {
                Id = id,
                Email = row.ContainsKey("Email") ? row["Email"] : null,
                Senha = row.ContainsKey("Senha") ? row["Senha"] : null,
            };

            _scenarioContext["UsuarioRequest"] = request;
        }

        [When("eu solicitar a edição do usuário")]
        public async Task WhenEuSolicitarAEdicaoDoUsuarioAsync()
        {
            var request = _scenarioContext.Get<UsuarioUpdateRequest>("UsuarioRequest");

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            _response = await Client.PostAsync("/usuario/Editar", content);

            _scenarioContext["Response"] = _response;
        }

        [Given("que eu preencho os dados para editar o usuário {int} com o e-mail {string}")]
        public void GivenQueEuPreenchoOsDadosParaEditarOUsuarioComOE_Mail(int id, string email)
        {
            var request = new UsuarioUpdateRequest { Id = id, Email = email };

            _scenarioContext["UsuarioRequest"] = request;
        }

        [BeforeScenario]
        public void ClearMocks()
        {
            _usuarioRepositoryMock.Invocations.Clear();
            _usuarioRepositoryMock.Reset();
        }
    }
}
