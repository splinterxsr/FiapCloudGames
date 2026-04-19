using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Tests.Support.TestDouble
{
    internal class UsuarioTesteAuth : Usuario
    {
        internal UsuarioTesteAuth(string nome, string email, string senha, int perfil)
            : base(nome, email, senha, perfil)
        {
        }

        /// <summary>
        /// Método criado para que pudessemos definir valor para as propriedades privadas 'Status' e 'Perfil'
        /// sem precisar alterar a classe original 'Usuario'
        /// </summary>
        internal void DefinirSituacaoEPerfil(char status, EnumPerfil perfilEnum)
        {
            var padrao = "<campo>k__BackingField";
            var bindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;

            var campoSituacao = typeof(BaseEntity).GetField(padrao.Replace("campo", "Situacao"), bindingFlags);
            campoSituacao?.SetValue(this, status);

            var campoPerfil = typeof(Usuario).GetField(padrao.Replace("campo", "Perfil"), bindingFlags);
            campoPerfil?.SetValue(this, new Perfil((int)perfilEnum, perfilEnum.ToString()));
        }
    }
}
