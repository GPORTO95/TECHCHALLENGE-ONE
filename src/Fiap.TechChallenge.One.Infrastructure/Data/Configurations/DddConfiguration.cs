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

        builder.ComplexProperty(
            d => d.Estado,
            b => 
            {
                b.Property(e => e.Sigla).HasColumnName("SiglaEstado").HasMaxLength(2);
                b.Property(e => e.Descricao).HasColumnName("DescricaoEstado").HasMaxLength(100);
            });

        builder.ComplexProperty(
            d => d.CodigoRegiao,
            b =>
            {
                b.Property(e => e.Valor).HasColumnName("Codigo").HasMaxLength(2);
            });

        //builder.OwnsOne(d => d.CodigoRegiao, c =>
        //{
        //    c.Property(x => x.Valor).HasColumnName("Codigo");

        //    c.HasIndex(c => c.Valor).IsUnique(true);
        //});
    }
}
