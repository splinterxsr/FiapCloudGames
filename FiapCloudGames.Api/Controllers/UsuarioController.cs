using FiapCloudGames.Api.Models;
using FiapCloudGames.Api.Profiles;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers
{
    [Authorize(Policy = nameof(Policy.Administrador))]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaService _senhaService;
        private readonly Mapper _mapper;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, ISenhaService senhaService, Mapper mapper, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _senhaService = senhaService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("ObterTodos")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Obter(CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.ObterAsync(cancellationToken);

            var response = _mapper.Map(usuarios);

            return Ok(response);
        }

        [HttpGet("ObterPorId/{id}")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obter([FromRoute] int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(id, cancellationToken);

            if (usuario == null) return NotFound();

            var response = _mapper.Map(usuario);

            return Ok(response);
        }

        [HttpGet("ObterPorEmail/{email}")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obter([FromRoute] string email, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(email, cancellationToken);

            if (usuario == null) return NotFound();

            var response = _mapper.Map(usuario);

            return Ok(response);
        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioInsertRequest request, CancellationToken cancellationToken)
        {
            var usuarioExiste = await _usuarioRepository.ObterAsync(request.Email, cancellationToken);

            if (usuarioExiste != null) return Conflict(new { Mensagem = "O e-mail inserido já está atrelado a outro cadastro."});

            _logger.LogInformation($"Adicionando novo usuário '{request.Nome}'.");

            var hashSenha = _senhaService.CriaHash(request.Senha);

            request.Senha = hashSenha;

            var usuario = _mapper.Map(request);

            await _usuarioRepository.AdicionarAsync(usuario, cancellationToken);

            _logger.LogInformation($"O usuário '{request.Nome}' foi adicionado com sucesso.");

            return Created(string.Empty, usuario);
        }

        [HttpPost("Editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Editar([FromBody] UsuarioUpdateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando edição do usuário '{request.Nome}' (ID: {request.Id}).");

            var usuario = await _usuarioRepository.ObterAsync(request.Id, cancellationToken);

            if (usuario == null) return NotFound();

            if (!string.IsNullOrEmpty(request.Email))
            {
                var usuarioEmailExiste = await _usuarioRepository.ObterAsync(request.Email, cancellationToken);

                if (usuarioEmailExiste != null && usuarioEmailExiste.Id != request.Id) return Conflict(new { Mensagem = "O e-mail inserido já está atrelado a outro cadastro." });
            }

            var senhaHash = request.Senha is null ? null : _senhaService.CriaHash(request.Senha);

            usuario.Atualizar(request.Nome, request.Email, senhaHash, request.PerfilId);

            await _usuarioRepository.EditarAsync(usuario, cancellationToken);

            _logger.LogInformation($"O usuário '{request.Nome}' (ID: {request.Id}) foi editado com sucesso.");

            return Ok();
        }

        [HttpPost("Inativar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Inativar([FromRoute] int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterAsync(id, cancellationToken);

            if (usuario == null) return NotFound();

            _logger.LogInformation($"Iniciando inativação do usuário '{usuario.Nome}' (ID: {usuario.Id}).");

            await _usuarioRepository.InativarAsync(id, cancellationToken);

            _logger.LogInformation($"O usuário '{usuario.Nome}' (ID: {usuario.Id}) foi inativado com sucesso.");

            return Ok();
        }
    }
}
