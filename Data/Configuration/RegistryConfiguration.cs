using canalTransacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace canalTransacao.Data
{
    public class RegistryConfiguration : IEntityTypeConfiguration<Registry>
    {
        public void Configure(EntityTypeBuilder<Registry> builder)
        {
            builder.ToTable("Registro");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Data).IsRequired().HasColumnType("datetime");
            builder.Property(r => r.Canal).IsRequired().HasMaxLength(16).HasColumnType("varchar(10)");
            builder.Property(r => r.Transacao).IsRequired().HasMaxLength(256).HasColumnType("varchar(256)");
            builder.Property(r => r.Quantidade).IsRequired().HasColumnType("int");
            builder.Property(r => r.Valor).HasColumnType("decimal").HasPrecision(18, 2);
            builder.Property(r => r.Grupo).HasColumnType("int");
        }
    }
}