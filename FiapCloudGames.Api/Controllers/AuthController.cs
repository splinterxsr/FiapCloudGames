using FiapCloudGames.Api.Models;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FiapCloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaService _senhaService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUsuarioRepository usuarioRepository, ISenhaService senhaService, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _senhaService = senhaService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando processo de autenticação.");

            _logger.LogInformation($"Validando as credenciais do usuário '{request.Email}'.");

            var usuario = await _usuarioRepository.ObterAsync(request.Email, cancellationToken);

            if (usuario == null)
            {
                _logger.LogInformation("Credenciais inválidas! Usuário ou senha incorretos.");
                return Unauthorized();
            }

            var senhaValida = _senhaService.ValidaSenha(request.Senha, usuario.Senha);

            if (!senhaValida)
            {
                _logger.LogInformation("Credenciais inválidas! Usuário ou senha incorretos..");
                return Unauthorized();
            }

            _logger.LogInformation("Credenciais validadas com sucesso.");

            _logger.LogInformation($"Criando token JWT para o usuário '{request.Email}'.");

            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection.GetValue<string>("Key");
            var issuer = jwtSection.GetValue<string>("Issuer");
            var audience = jwtSection.GetValue<string>("Audience");
            var expiresMinutes = jwtSection.GetValue<int>("ExpiresMinutes");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.PerfilId.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            _logger.LogInformation("Token JWT criado com sucesso.");

            _logger.LogInformation("Usuário autenticado com sucesso.");

            return Ok(new LoginResponse { Token = tokenString, Expires = DateTime.UtcNow.AddMinutes(expiresMinutes) });
        }
    }
}