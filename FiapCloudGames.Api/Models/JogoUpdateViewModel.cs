using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Api.Models
{
    public class JogoUpdateViewModel : JogoViewModel
    {
        [Required(ErrorMessage = "É obrigatório informar o ID do jogo.")]
        public int Id { get; set; }
    }
}
