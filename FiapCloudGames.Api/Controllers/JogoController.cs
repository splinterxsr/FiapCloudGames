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
        private readonly ILogger<JogoController> _logger;

        public JogoController(IJogoRepository jogoRepository, Mapper mapper, ILogger<JogoController> logger)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("ObterTodos")]
        [ProducesResponseType(typeof(IEnumerable<JogoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Obter(CancellationToken cancellationToken)
        {
            var jogos = await _jogoRepository.ObterAsync(cancellationToken);

            var response = _mapper.Map(jogos);

            return Ok(response);
        }

        [HttpGet("ObterPorId/{id}")]
        [ProducesResponseType(typeof(JogoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obter([FromRoute] int id, CancellationToken cancellationToken)
        {
            var jogo = await _jogoRepository.ObterAsync(id, cancellationToken);

            if (jogo == null) return NotFound();

            var response = _mapper.Map(jogo);

            return Ok(response);
        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Adicionar([FromBody] JogoInsertRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Adicionando novo jogo '{request.Nome}'.");

            var jogo = _mapper.Map(request);

            await _jogoRepository.AdicionarAsync(jogo, cancellationToken);

            _logger.LogInformation($"O jogo '{request.Nome}' foi adicionado com sucesso.");

            return Created();
        }

        [HttpPost("Editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Editar([FromBody] JogoUpdateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando edição do jogo '{request.Nome}' (ID: {request.Id}).");

            var jogo = await _jogoRepository.ObterAsync(request.Id, cancellationToken);

            if (jogo == null) return NotFound();

            jogo.Atualizar(request.Nome);

            await _jogoRepository.EditarAsync(jogo, cancellationToken);

            _logger.LogInformation($"O jogo '{request.Nome}' (ID: {request.Id}) foi editado com sucesso.");

            return Ok();
        }

        [HttpPost("Inativar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Inativar([FromRoute] int id, CancellationToken cancellationToken)
        {
            var jogo = await _jogoRepository.ObterAsync(id, cancellationToken);

            if (jogo == null) return NotFound();

            _logger.LogInformation($"Iniciando inativação do jogo '{jogo.Nome}' (ID: {jogo.Id}).");

            await _jogoRepository.InativarAsync(id, cancellationToken);

            _logger.LogInformation($"O jogo '{jogo.Nome}' (ID: {jogo.Id}) foi inativado com sucesso.");

            return Ok();
        }
    }
}
