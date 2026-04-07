namespace FiapCloudGames.Domain.Services
{
    public interface ISenhaService
    {
        string CriaHash(string senha);
        bool ValidaSenha(string senha, string hash);
    }
}
