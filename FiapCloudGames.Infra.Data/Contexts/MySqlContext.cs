using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Services;
using FiapCloudGames.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FiapCloudGames.Infra.Data.Contexts
{
    public class MySqlContext : DbContext
    {
        private readonly ISenhaService _senhaService;
        private readonly IConfiguration _configuration;

        public MySqlContext(ISenhaService senhaService, IConfiguration configuration, DbContextOptions<MySqlContext> options) : base(options)
        {
            _senhaService = senhaService;
            _configuration = configuration;
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString(nameof(Contexts.Database.MySql));

                if (string.IsNullOrEmpty(connectionString)) throw new InvalidOperationException($"Connection string para o banco {nameof(Contexts.Database.MySql)} não encontrada.");

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JogoConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration(_senhaService));
        }
    }
}