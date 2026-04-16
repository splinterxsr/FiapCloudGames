namespace FiapCloudGames.Api.Models
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string PerfilNome { get; set; } = null!;
        public DateTime DataHora { get; set; }
    }
}
