using FiapCloudGames.Infra.CrossCutting.Security;
using FiapCloudGames.Tests.Support;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

public static class TokenGenerator
{
    private static readonly JwtService _jwtService;

    static TokenGenerator()
    {
        var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        var options = config.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        var jwtOptions = Options.Create(options);

        _jwtService = new JwtService(jwtOptions);
    }

    public static string GenerateToken(int perfilId)
    {
        var perfil = (EnumPerfil)perfilId;

        return _jwtService.GerarToken("email.teste@fiap.com", "Usuário Teste", perfil.ToString());
    }
}