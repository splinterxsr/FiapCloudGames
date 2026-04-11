using FiapCloudGames.Api.Models;
using FiapCloudGames.Api.Profiles;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaService _senhaService;
        private readonly Mapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, ISenhaService senhaService, Mapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _senhaService = senhaService;
            _mapper = mapper;
        }

        [HttpGet("ObterTodos")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Obter(CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.ObterAsync(cancellationToken);

            var viewModel = _mapper.Map(usuarios);

            return Ok(viewModel);
        }

        [HttpGet("ObterPorId/{id}")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obter([FromRoute] int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(id, cancellationToken);

            if (usuario == null) return NotFound();

            var viewModel = _mapper.Map(usuario);

            return Ok(viewModel);
        }

        [HttpGet("ObterPorEmail/{email}")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obter([FromRoute] string email, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(email, cancellationToken);

            if (usuario == null) return NotFound();

            var viewModel = _mapper.Map(usuario);

            return Ok(viewModel);
        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioViewModel model, CancellationToken cancellationToken)
        {
            var hashSenha = _senhaService.CriaHash(model.Senha);

            model.Senha = hashSenha;

            var usuario = _mapper.Map(model);

            await _usuarioRepository.AdicionarAsync(usuario, cancellationToken);

            return Created();
        }

        [HttpPost("Editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Editar([FromBody] UsuarioUpdateViewModel model, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(model.Id, cancellationToken);

            if (usuario == null) return NotFound();

            var senhaHash = model.Senha is null ? null : _senhaService.CriaHash(model.Senha);

            usuario.Atualizar(model.Nome, model.Email, senhaHash, model.PerfilId);

            await _usuarioRepository.EditarAsync(usuario, cancellationToken);

            return Ok();
        }

        [HttpPost("Inativar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Inativar([FromRoute] int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(id, cancellationToken);

            if (usuario == null) return NotFound();

            await _usuarioRepository.InativarAsync(id, cancellationToken);

            return Ok();
        }
    }
}
