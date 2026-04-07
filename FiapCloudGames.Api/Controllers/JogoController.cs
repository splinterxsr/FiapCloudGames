using FiapCloudGames.Api.Models;
using FiapCloudGames.Api.Profiles;
using FiapCloudGames.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly Mapper _mapper;

        public JogoController(IJogoRepository jogoRepository, Mapper mapper)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        [HttpGet("ObterTodos")]
        [ProducesResponseType(typeof(IEnumerable<JogoViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Obter()
        {
            var jogos = await _jogoRepository.ObterAsync();

            var viewModel = _mapper.Map(jogos);

            return Ok(viewModel);
        }

        [HttpGet("ObterPorId/{id}")]
        [ProducesResponseType(typeof(JogoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obter(int id)
        {
            var jogo = await _jogoRepository.ObterAsync(id);

            if (jogo == null) return NotFound();

            var viewModel = _mapper.Map(jogo);

            return Ok(viewModel);
        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Adicionar(JogoViewModel model)
        {
            var jogo = _mapper.Map(model);

            await _jogoRepository.AdicionarAsync(jogo);

            return Created();
        }

        [HttpPost("Editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Editar(JogoUpdateViewModel model)
        {
            var jogo = await _jogoRepository.ObterAsync(model.Id);

            if (jogo == null) return NotFound();

            jogo.Atualizar(model.Nome);

            await _jogoRepository.EditarAsync(jogo);

            return Ok();
        }

        [HttpPost("Inativar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Inativar(int id)
        {
            var jogo = await _jogoRepository.ObterAsync(id);

            if (jogo == null) return NotFound();

            await _jogoRepository.InativarAsync(id);

            return Ok();
        }
    }
}
