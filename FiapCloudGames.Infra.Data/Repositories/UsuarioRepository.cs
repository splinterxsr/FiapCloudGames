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
    
        public new async Task<Usuario?> ObterAsync(int id, CancellationToken cancellationToken = default) => await _dbSet.Include(u => u.Perfil).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        public async Task<Usuario?> ObterAsync(string email, CancellationToken cancellationToken = default) => await _dbSet.Include(u => u.Perfil).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        public new async Task<IEnumerable<Usuario>> ObterAsync(CancellationToken cancellationToken = default) => await _dbSet.Include(u => u.Perfil).Where(t => t.Situacao == 'A').ToListAsync(cancellationToken);
    }
}