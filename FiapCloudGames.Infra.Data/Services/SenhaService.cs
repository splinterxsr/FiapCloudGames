using FiapCloudGames.Domain.Services;

namespace FiapCloudGames.Infra.Data.Services
{
    public class SenhaService : ISenhaService
    {
        public string CriaHash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 12);
        }

        public bool ValidaSenha(string password, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }
    }
}
