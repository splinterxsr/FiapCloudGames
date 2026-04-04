using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infra.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MySqlContext context): base(context)
        {
        }

        public async Task<Usuario?> ObterAsync(string email, CancellationToken cancellationToken = default) => await _dbSet.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}