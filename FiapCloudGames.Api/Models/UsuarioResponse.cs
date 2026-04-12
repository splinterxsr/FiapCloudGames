namespace FiapCloudGames.Api.Models
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string PerfilNome { get; set; }
        public DateTime DataHora { get; set; }
    }
}
