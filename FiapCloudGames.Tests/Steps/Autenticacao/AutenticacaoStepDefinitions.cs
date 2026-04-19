using FiapCloudGames.Api.Models;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Infra.Data.Services;
using FiapCloudGames.Tests.Factory;
using FiapCloudGames.Tests.Support;
using FiapCloudGames.Tests.Support.TestDouble;
using Moq;
using Reqnroll;
using System.Text;
using System.Text.Json;

[Binding]
public class AutenticacaoStepDefinitions
{
    private readonly HttpClient _client;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly SenhaService _senhaService = new();
    private readonly ScenarioContext _scenarioContext;

    private LoginRequest _loginRequest;
    private HttpResponseMessage _response;

    public AutenticacaoStepDefinitions(WebAppFactory<Program> factory, ScenarioContext scenarioContext)
    {
        _usuarioRepositoryMock = factory.UsuarioRepositoryMock;
        _client = factory.CreateClient();
        _scenarioContext = scenarioContext;
    }

    [Given("que existe um usuário cadastrado com e-mail {string} e senha {string}")]
    public void GivenQueExisteUmUsuarioCadastradoComE_MailESenha(string email, string senha)
    {
        var usuario = new UsuarioTesteAuth("Teste", email, _senhaService.CriaHash(senha), 1);
        usuario.DefinirSituacaoEPerfil('A', EnumPerfil.Administrador);

        _usuarioRepositoryMock.Setup(r => r.ObterAsync(email, It.IsAny<CancellationToken>())).ReturnsAsync(usuario);
    }

    [Given("que eu informo o e-mail {string} e a senha {string}")]
    public void GivenQueEuInformoOE_MailEASenha(string email, string senha)
    {
        _loginRequest = new LoginRequest { Email = email, Senha = senha };
    }

    [When("eu solicitar o login")]
    public async Task WhenEuSolicitarOLogin()
    {
        var content = new StringContent(JsonSerializer.Serialize(_loginRequest), Encoding.UTF8, "application/json");
        _response = await _client.PostAsync("/auth/login", content);

        _scenarioContext["Response"] = _response;
    }

    [Then("deve conter um token JWT válido na resposta")]
    public async Task ThenDeveConterUmTokenJWTValido()
    {
        var body = await _response.Content.ReadAsStringAsync();
        Assert.Contains("token", body);
    }

    [BeforeScenario]
    public void ResetMocks()
    {
        _usuarioRepositoryMock.Invocations.Clear();
        _usuarioRepositoryMock.Reset();
    }
}
