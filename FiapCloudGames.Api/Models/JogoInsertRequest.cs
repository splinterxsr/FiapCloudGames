using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Api.Models
{
    public class JogoInsertRequest
    {
        [Required(ErrorMessage = "Insira o nome do jogo.")]
        public string Nome { get; set; } = null!;
    }
}
