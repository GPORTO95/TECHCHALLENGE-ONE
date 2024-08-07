using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TechChallenge.Listagem.API.Repositories;

internal sealed class ContatoRepository(ApplicationDbContext dbContext) : IContatoRepository
{
    public async Task<List<Contato>> ListarAsync(Codigo? codigo, CancellationToken cancellationToken = default)
    {
        IQueryable<Contato> query = dbContext.Contatos.AsNoTracking();

        if (codigo is not null)
        {
            query = query.Where(c => c.Ddd.CodigoRegiao == codigo);
        }

        return await query.Include(c => c.Ddd).ToListAsync(cancellationToken);
    }
}