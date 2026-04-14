using Reqnroll;

namespace FiapCloudGames.Tests.Steps.Usuarios
{
    [Binding]
    public class EditarUsuarioStepDefinitions
    {
        [Given("existe um usuário cadastrado com o ID {int} e e-mail {string}")]
        public void GivenExisteUmUsuarioCadastradoComOIDEE_Mail(int p0, string p1)
        {
            throw new PendingStepException();
        }

        [When("eu solicitar a edição do usuário {int} com os dados:")]
        public void WhenEuSolicitarAEdicaoDoUsuarioComOsDados(int p0, DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [When("eu solicitar a edição do usuário {int} trocando o {string} para {string}")]
        public void WhenEuSolicitarAEdicaoDoUsuarioTrocandoOPara(int p0, string email, string p2)
        {
            throw new PendingStepException();
        }

        [Given("que existe outro usuário cadastrado com o e-mail {string}")]
        public void GivenQueExisteOutroUsuarioCadastradoComOE_Mail(string p0)
        {
            throw new PendingStepException();
        }

        [When("eu solicitar a edição do usuário {int} trocando seu e-mail para {string}")]
        public void WhenEuSolicitarAEdicaoDoUsuarioTrocandoSeuE_MailPara(int p0, string p1)
        {
            throw new PendingStepException();
        }

        [When("eu solicitar a edição do usuário {int}")]
        public void WhenEuSolicitarAEdicaoDoUsuario(int p0)
        {
            throw new PendingStepException();
        }

    }
}
