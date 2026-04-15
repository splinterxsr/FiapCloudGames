using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Api.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Informe o e-mail.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O e-mail inserido é inválido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Informe a senha.")]
        public string Senha { get; set; } = null!;
    }
}