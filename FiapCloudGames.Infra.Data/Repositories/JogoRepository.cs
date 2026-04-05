using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Infra.Data.Contexts;

namespace FiapCloudGames.Infra.Data.Repositories
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(MySqlContext context) : base(context)
        {
        }
    }
}
