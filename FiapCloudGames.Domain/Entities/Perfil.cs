namespace FiapCloudGames.Domain.Entities
{
    public class Perfil : EntityBase
    {
        /// <summary>
        /// Construtor usado pelo Entity Framework Core. Não deve ser usado diretamente.
        /// </summary>
        protected Perfil()
        {
        }

        public Perfil(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}