using Fiap.TechChallenge.Kernel.Contatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.TechChallenge.Infrastructure.Data.Configurations;

internal sealed class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");

        builder.HasKey(c => c.Id);

        builder.ComplexProperty(
            c => c.Email,
            b => b.Property(e => e.Value).HasColumnName("Email").HasMaxLength(100));

        builder.ComplexProperty(
            c => c.Nome,
            b => b.Property(e => e.Value).HasColumnName("Nome").HasMaxLength(200));

        builder.ComplexProperty(
            c => c.Telefone,
            b => b.Property(e => e.Value).HasColumnName("Telefone").HasMaxLength(9));

        builder.HasOne(c => c.Ddd)
            .WithMany(d => d.Contatos)
            .HasForeignKey(c => c.DddId);
    }
}
