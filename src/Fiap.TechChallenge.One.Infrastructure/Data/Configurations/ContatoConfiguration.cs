using Fiap.TechChallenge.One.Domain.Contatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.TechChallenge.One.Infrastructure.Data.Configurations;

internal sealed class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");

        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.DddId).IsUnique(false);

        builder.ComplexProperty(
            c => c.Email,
            b => b.Property(e => e.Value).HasColumnName("Email").HasMaxLength(100));

        builder.ComplexProperty(
            c => c.Nome,
            b => b.Property(e => e.Value).HasColumnName("Nome").HasMaxLength(200));

        builder.ComplexProperty(
            c => c.Telefone,
            b => b.Property(e => e.Value).HasColumnName("Telefone").HasMaxLength(9));

        builder.HasOne(d => d.Ddd)
            .WithOne()
            .HasForeignKey<Contato>(c => c.DddId);
    }
}
