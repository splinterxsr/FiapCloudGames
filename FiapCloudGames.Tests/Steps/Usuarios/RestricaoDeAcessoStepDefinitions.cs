using FiapCloudGames.Api.Models;
using Reqnroll;
using System.Text;
using System.Text.Json;

namespace FiapCloudGames.Tests.Steps.Usuarios
{
    [Binding]
    public class RestricaoDeAcessoStepDefinitions
    {
        private HttpResponseMessage _response;
        private readonly ScenarioContext _scenarioContext;

        public RestricaoDeAcessoStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private HttpClient Client => _scenarioContext.Get<HttpClient>("HttpClient");

        private record ApiOperation(HttpMethod Method, string Path, object? DefaultBody = null);

        private static readonly Dictionary<string, ApiOperation> operacoes = new()
        {
            { "listar todos os usuários",    new(HttpMethod.Get,  "/usuario/ObterTodos") },
            { "editar o usuário 2",          new(HttpMethod.Post, "/usuario/Editar", new UsuarioUpdateRequest{ Id = 2, Nome = "Usuário Teste" }) },
            { "inativar o usuário 2",        new(HttpMethod.Post, "/usuario/Inativar/2") },
            { "buscar o usuário por id 2",   new(HttpMethod.Get,  "/usuario/ObterPorId/2") },
            { "adicionar jogo novo",         new(HttpMethod.Post, "/jogo/Adicionar", new JogoInsertRequest{ Nome = "Jogo Proibido" }) },
            { "editar o jogo 1",             new(HttpMethod.Post, "/jogo/Editar", new JogoUpdateRequest{ Id = 1, Nome = "Jogo Proibido" }) },
            { "inativar o jogo 1",           new(HttpMethod.Post, "/jogo/inativar/1") }
        };

        [When(@"eu tentar realizar a operação (.*)")]
        public async Task WhenEuTentarRealizarAOperacao(string operacao)
        {
            var chave = operacao.Trim().ToLower();

            var config = operacoes[chave];

            using var request = new HttpRequestMessage(config.Method, config.Path);

            if (config.Method == HttpMethod.Post || config.Method == HttpMethod.Put)
            {
                var body = config.DefaultBody ?? new { };
                var json = JsonSerializer.Serialize(body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await Client.SendAsync(request);

            _scenarioContext["Response"] = response;
        }
    }
}
