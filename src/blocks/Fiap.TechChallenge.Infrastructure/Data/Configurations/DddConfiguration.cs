using Fiap.TechChallenge.Kernel.Ddds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.TechChallenge.Infrastructure.Data.Configurations;

internal sealed class DddConfiguration : IEntityTypeConfiguration<Ddd>
{
    public void Configure(EntityTypeBuilder<Ddd> builder)
    {
        builder.ToTable("Ddds");

        builder.HasKey(d => d.Id);

        builder.ComplexProperty(
            d => d.Estado,
            b => 
            {
                b.Property(e => e.Sigla).HasColumnName("SiglaEstado").HasMaxLength(2);
                b.Property(e => e.Descricao).HasColumnName("DescricaoEstado").HasMaxLength(100);
            });

        builder
            .Property(x => x.CodigoRegiao)
            .HasConversion(x => x.Valor, v => Codigo.Criar(v).Value)
            .HasColumnName("Codigo");

        builder
            .HasIndex(c => c.CodigoRegiao)
            .IsUnique(true);
    }
}
