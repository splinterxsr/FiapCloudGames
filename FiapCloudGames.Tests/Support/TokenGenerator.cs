using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public static class TokenGenerator
{
    public static string GenerateToken(string perfilId)
    {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, "UsuarioTeste"),
            new Claim(ClaimTypes.Email, "teste@fiap.com"),
            new Claim(ClaimTypes.Role, perfilId)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SubstituaPorUmaChaveSecretaMuitoForteEArmazeneEmSegredo"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "FiapCloudGames",
            audience: "FiapCloudGames",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}