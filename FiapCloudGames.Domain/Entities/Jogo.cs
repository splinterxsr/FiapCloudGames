namespace FiapCloudGames.Domain.Entities
{
    public class Jogo : EntityBase
    {
        /// <summary>
        /// Construtor usado pelo Entity Framework Core. Não deve ser usado diretamente.
        /// </summary>
        protected Jogo()
        {
        }

        public Jogo(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Jogo(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
        }
    }
}