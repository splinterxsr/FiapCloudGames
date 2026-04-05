using FiapCloudGames.Api.Models;
using FiapCloudGames.Api.Profiles;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly Mapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, Mapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet("ObterTodos")]
        public IActionResult Obter()
        {
            var usuarios = _usuarioRepository.ObterAsync().Result;

            var viewModel = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(usuarios);

            return Ok(viewModel);
        }

        [HttpGet("ObterPorId/{id}")]
        public IActionResult Obter(int id)
        {
            var usuario = _usuarioRepository.ObterAsync(id).Result;

            if (usuario == null) return NotFound();

            var viewModel = _mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return Ok(viewModel);
        }

        [HttpGet("ObterPorEmail/{email}")]
        public IActionResult Obter(string email)
        {
            var usuario = _usuarioRepository.ObterAsync(email).Result;

            if (usuario == null) return NotFound();

            var viewModel = _mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return Ok(viewModel);
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> Adicionar(UsuarioViewModel model)
        {
            var usuario = _mapper.Map<UsuarioViewModel, Usuario>(model);

            await _usuarioRepository.AdicionarAsync(usuario);

            return Created();
        }

        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(UsuarioUpdateViewModel model)
        {
            var usuario = _usuarioRepository.ObterAsync(model.Id).Result;

            if (usuario == null) return NotFound();

            usuario.Atualizar(model.Nome, model.Email, model.Senha, model.PerfilId);

            await _usuarioRepository.EditarAsync(usuario);

            return Ok();
        }

        [HttpPost("Inativar/{id}")]
        public async Task<IActionResult> Inativar(int id)
        {
            await _usuarioRepository.InativarAsync(id);

            return Ok();
        }
    }
}
