using Reqnroll;

namespace FiapCloudGames.Tests.Steps.Usuarios
{
    [Binding]
    public class RestricaoDeAcessoStepDefinitions
    {
        [When("eu tentar realizar a operação {string}")]
        public void WhenEuTentarRealizarAOperacao(string p0)
        {
            throw new PendingStepException();
        }

        [Then("o sistema deve recusar o acesso com o status {int} Forbidden")]
        public void ThenOSistemaDeveRecusarOAcessoComOStatusForbidden(int p0)
        {
            throw new PendingStepException();
        }

    }
}
