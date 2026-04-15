using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Exceptions;
using FiapCloudGames.Domain.Repositories;

namespace FiapCloudGames.Domain.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaService _senhaService;

        public UsuarioService(IUsuarioRepository usuarioRepository, ISenhaService senhaService)
        {
            _usuarioRepository = usuarioRepository;
            _senhaService = senhaService;
        }

        public async Task<Usuario> Autenticar(string email, string senha, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(email, cancellationToken) ?? throw new SemAutorizacaoException("Usuário e/ou senha inválidos.");

            if (usuario.Situacao != 'A') throw new SemAutorizacaoException("Usuário está desativado no sistema.");

            var senhaValida = _senhaService.ValidaSenha(senha, usuario.Senha);

            if (!senhaValida) throw new SemAutorizacaoException("Usuário e/ou senha inválidos.");

            return usuario;
        }
    }
}