namespace FiapCloudGames.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public string Nome { get; protected set; } = null!;
        public char Situacao { get; private set; }
        public DateTime DataHora { get; protected set; }

        public void Inativar() => Situacao = 'I';
    }
}