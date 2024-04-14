using Fiap.TechChallenge.One.Domain.Ddds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.TechChallenge.One.Infrastructure.Data.Configurations;

internal sealed class DddConfiguration : IEntityTypeConfiguration<Ddd>
{
    public void Configure(EntityTypeBuilder<Ddd> builder)
    {
        builder.ToTable("Ddds");

        builder.HasKey(d => d.Id);

        builder.HasIndex(d => d.Estado.Sigla).IsUnique();

        builder.ComplexProperty(
            d => d.Estado,
            b => 
            {
                b.Property(e => e.Sigla).HasColumnName("SiglaEstado").HasMaxLength(2);
                b.Property(e => e.Descricao).HasColumnName("DescricaoEstado").HasMaxLength(100);
            });
    }
}
