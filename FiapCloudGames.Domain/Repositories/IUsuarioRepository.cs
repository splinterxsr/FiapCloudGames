using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        /// <summary>
        /// Obter usuário por e-mail.
        /// </summary>
        /// <param name="email">E-mail de acesso do usuário.</param>
        /// <returns></returns>
        Task<Usuario?> ObterAsync(string email, CancellationToken cancellationToken = default);
    }
}
