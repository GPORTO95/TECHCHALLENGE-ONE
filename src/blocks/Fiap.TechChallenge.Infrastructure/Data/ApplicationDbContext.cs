using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TechChallenge.Infrastructure.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    public DbSet<Contato> Contatos { get; set; }

    public DbSet<Ddd> Ddds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}
