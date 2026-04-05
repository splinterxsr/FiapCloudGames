using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Api.Models
{
    public class JogoViewModel
    {
        [Required(ErrorMessage = "Insira o nome do jogo.")]
        public string Nome { get; set; } = null!;
    }
}
