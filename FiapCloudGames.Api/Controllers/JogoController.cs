using FiapCloudGames.Api.Models;
using FiapCloudGames.Api.Profiles;
using FiapCloudGames.Domain.Entities;
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
        public IActionResult Obter()
        {
            var jogos = _jogoRepository.ObterAsync().Result;

            var viewModel = _mapper.Map<IEnumerable<Jogo>, IEnumerable<JogoViewModel>>(jogos);

            return Ok(viewModel);
        }

        [HttpGet("ObterPorId/{id}")]
        public IActionResult Obter(int id)
        {
            var jogo = _jogoRepository.ObterAsync(id).Result;

            if (jogo == null) return NotFound();

            var viewModel = _mapper.Map<Jogo, JogoViewModel>(jogo);

            return Ok(viewModel);
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> Adicionar(JogoViewModel model)
        {
            var jogo = _mapper.Map<JogoViewModel, Jogo>(model);

            await _jogoRepository.AdicionarAsync(jogo);

            return Created();
        }

        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(JogoUpdateViewModel model)
        {
            var jogo = _jogoRepository.ObterAsync(model.Id).Result;

            if (jogo == null) return NotFound();

            jogo.Atualizar(model.Nome);

            await _jogoRepository.EditarAsync(jogo);

            return Ok();
        }

        [HttpPost("Inativar/{id}")]
        public async Task<IActionResult> Inativar(int id)
        {
            await _jogoRepository.InativarAsync(id);

            return Ok();
        }

    }
}
