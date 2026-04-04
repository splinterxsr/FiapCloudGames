namespace FiapCloudGames.Domain.Entities
{
    public class Usuario : EntityBase
    {
        /// <summary>
        /// Construtor usado pelo Entity Framework Core. Não deve ser usado diretamente.
        /// </summary>
        protected Usuario()
        {
        }

        public Usuario(int id, string nome, string email, string senha, int perfilId)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            PerfilId = perfilId;
        }

        public string Email { get; private set; } = null!;
        public string Senha { get; private set; } = null!;
        public int PerfilId { get; private set; }
        public Perfil? Perfil { get; private set; }
    }
}