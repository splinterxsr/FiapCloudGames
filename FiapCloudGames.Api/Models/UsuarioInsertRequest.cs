using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Api.Models
{
    public class UsuarioInsertRequest
    {
        [Required(ErrorMessage = "Insira o nome do usuário.")]
        public string Nome { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O e-mail inserido é inválido.")]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "A senha precisa ter no mínimo 8 dígitos, contendo números, letras e caracteres especiais.")]
        public string Senha { get; set; } = null!;

        [Required(ErrorMessage = "Insira o ID do perfil do usuário.")]
        public int PerfilId { get; set; }
    }
}
