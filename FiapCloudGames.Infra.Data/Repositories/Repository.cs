using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MySqlContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(MySqlContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> ObterAsync(int id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync([id], cancellationToken);

        public async Task<IEnumerable<T>> ObterAsync(CancellationToken cancellationToken = default) => await _dbSet.Where(t => t.Situacao == 'A').ToListAsync(cancellationToken);

        public async Task AdicionarAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task EditarAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task InativarAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await ObterAsync(id, cancellationToken);

            if (entity != null)
            {
                entity.Inativar();
                await EditarAsync(entity, cancellationToken);
            }
            else
            {
                throw new Exception("Registro não encontrado.");
            }
        }
    }
}