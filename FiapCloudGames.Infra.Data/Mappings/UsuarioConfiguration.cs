using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infra.Data.Mappings
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        private readonly ISenhaService _senhaService;

        public UsuarioConfiguration(ISenhaService senhaService)
        {
            _senhaService = senhaService;
        }

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .ToTable("tbl_usuario");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasColumnName("usuario_id")
                .HasColumnType("int(11)")
                .UseMySqlIdentityColumn()
                .IsRequired();

            builder
                .Property(u => u.Nome)
                .HasColumnName("usuario_nome")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasColumnName("usuario_email")
                .HasColumnType("varchar(40)")
                .IsRequired();

            builder
                .Property(u => u.Senha)
                .HasColumnName("usuario_senha")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .Property(u => u.Situacao)
                .HasColumnName("usuario_situacao")
                .HasColumnType("char(1)")
                .HasDefaultValue("A")
                .IsRequired();

            builder
                .Property(u => u.PerfilId)
                .HasColumnName("perfil_id")
                .HasColumnType("smallint(2)")
                .IsRequired();

            builder
                .Property(u => u.DataHora)
                .HasColumnName("usuario_datahora")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();

            builder
                .HasOne(u => u.Perfil)
                .WithMany()
                .HasForeignKey(u => u.PerfilId);

            builder
                .HasData(new Usuario(1, "Admin", "admin@fiapcloud.com.br", _senhaService.CriaHash("admin"), 1));
        }
    }
}
