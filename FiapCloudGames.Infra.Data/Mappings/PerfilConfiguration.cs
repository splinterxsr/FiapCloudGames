using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infra.Data.Mappings
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder
                .ToTable("tbl_perfil");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("perfil_id")
                .HasColumnType("smallint(2)")
                .UseMySqlIdentityColumn()
                .IsRequired();

            builder
                .Property(p => p.Nome)
                .HasColumnName("perfil_nome")
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Ignore(p => p.Situacao);

            builder
                .Property(p => p.DataHora)
                .HasColumnName("perfil_datahora")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();

            builder
                .HasData(
                    new Perfil(1, "Administrador"),
                    new Perfil(2, "Usuário")
                );
        }
    }
}