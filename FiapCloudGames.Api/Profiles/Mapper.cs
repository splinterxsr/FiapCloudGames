using FiapCloudGames.Api.Models;
using FiapCloudGames.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace FiapCloudGames.Api.Profiles
{
    [Mapper]
    public partial class Mapper
    {
        [MapperIgnoreSource(nameof(Usuario.PerfilId))]
        [MapperIgnoreSource(nameof(Usuario.Situacao))]
        public partial UsuarioResponse Map(Usuario source);

        public partial Usuario Map(UsuarioInsertRequest source);
        public partial Usuario Map(UsuarioUpdateRequest source);

        [MapperIgnoreSource(nameof(Jogo.Situacao))]
        public partial JogoResponse Map(Jogo source);

        public partial Jogo Map(JogoInsertRequest source);
        public partial Jogo Map(JogoUpdateRequest source);

        public partial IEnumerable<UsuarioResponse> Map(IEnumerable<Usuario> source);
        public partial IEnumerable<JogoResponse> Map(IEnumerable<Jogo> source);
    }
}
