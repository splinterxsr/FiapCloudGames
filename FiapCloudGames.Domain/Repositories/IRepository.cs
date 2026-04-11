using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> ObterAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> ObterAsync(CancellationToken cancellationToken = default);
        Task AdicionarAsync(T entity, CancellationToken cancellationToken = default);
        Task EditarAsync(T entity, CancellationToken cancellationToken = default);
        Task InativarAsync(int id, CancellationToken cancellationToken = default);
    }
}