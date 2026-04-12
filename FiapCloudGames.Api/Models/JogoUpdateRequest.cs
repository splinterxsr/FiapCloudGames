using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Api.Models
{
    public class JogoUpdateRequest
    {
        [Required(ErrorMessage = "É obrigatório informar o ID do jogo.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o nome do jogo.")]
        public string Nome { get; set; } = null!;
    }
}
