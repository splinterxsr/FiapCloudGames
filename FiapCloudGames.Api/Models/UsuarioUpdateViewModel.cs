using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Api.Models
{
    public class UsuarioUpdateViewModel
    {
        [Required(ErrorMessage = "É obrigatório informar o ID do usuário.")]
        public int Id { get; set; }
        public string? Nome { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O e-mail inserido é inválido.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "A senha precisa ter no mínimo 8 dígitos, contendo números, letras e caracteres especiais.")]
        public string? Senha { get; set; }
        public int? PerfilId { get; set; }
    }
}
