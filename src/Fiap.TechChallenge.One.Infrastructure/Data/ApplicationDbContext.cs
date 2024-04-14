using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Domain.Contatos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TechChallenge.One.Infrastructure.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}
