using FiapCloudGames.Api.Models;
using FiapCloudGames.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace FiapCloudGames.Api.Profiles
{
    [Mapper]
    public partial class Mapper
    {
        public partial TTarget Map<TSource, TTarget>(TSource source);
        public partial IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> source);

        [MapperIgnoreSource(nameof(Usuario.Perfil))]
        [MapperIgnoreSource(nameof(Usuario.Situacao))]
        [MapperIgnoreSource(nameof(Usuario.Id))]
        [MapperIgnoreSource(nameof(Usuario.DataHora))]
        public partial UsuarioViewModel Map(Usuario source);

        public partial Usuario Map(UsuarioViewModel source);
        public partial Usuario Map(UsuarioUpdateViewModel source);

        [MapperIgnoreSource(nameof(Jogo.Situacao))]
        [MapperIgnoreSource(nameof(Jogo.Id))]
        [MapperIgnoreSource(nameof(Jogo.DataHora))]
        public partial JogoViewModel Map(Jogo source);


        public partial Jogo Map(JogoViewModel source);

        public partial Jogo Map(JogoUpdateViewModel source);


        public partial IEnumerable<UsuarioViewModel> Map(IEnumerable<Usuario> source);

        public partial IEnumerable<JogoViewModel> Map(IEnumerable<Jogo> source);
    }
}
