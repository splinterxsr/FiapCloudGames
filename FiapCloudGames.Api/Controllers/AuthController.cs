using FiapCloudGames.Api.Documentation;
using FiapCloudGames.Api.Models;
using FiapCloudGames.Domain.Exceptions;
using FiapCloudGames.Domain.Services;
using FiapCloudGames.Infra.CrossCutting.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UsuarioService usuarioService, JwtService jwtService, ILogger<AuthController> logger)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
        [SwaggerOperation(Summary = "Realizar a autenticação e obter token de acesso.")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _usuarioService.Autenticar(request.Email, request.Senha, cancellationToken);

                var token = _jwtService.GerarToken(usuario.Email, usuario.Nome, usuario.Perfil!.Nome);

                var loginResponse = new LoginResponse { Email = usuario.Email, Token = token };

                _logger.LogInformation($"Usuário {request.Email} com permissão de acesso. Acesso liberado.");

                return Ok(loginResponse);
            }
            catch (SemAutorizacaoException ex)
            {
                _logger.LogInformation($"Usuário {request.Email} sem permissão de acesso. Acesso bloqueado. Motivo: {ex.Message}");

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar autenticar usuário {request.Email}. Motivo: {ex.Message}");

                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
        }
    }
}