using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infra.Data.Mappings
{
    public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder
                .ToTable("tbl_jogo");

            builder
                .HasKey(j => j.Id);

            builder
                .Property(j => j.Id)
                .HasColumnName("jogo_id")
                .HasColumnType("int(11)")
                .UseMySqlIdentityColumn()
                .IsRequired();

            builder
                .Property(j => j.Nome)
                .HasColumnName("jogo_nome")
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(j => j.Situacao)
                .HasColumnName("jogo_situacao")
                .HasColumnType("char(1)")
                .HasDefaultValue("A")
                .IsRequired();

            builder
                .Property(j => j.DataHora)
                .HasColumnName("jogo_datahora")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();
        }
    }
}