using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Kernel.Contatos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TechChallenge.Exclusao.API.Repositories;

internal sealed class ContatoRepository(ApplicationDbContext dbContext) : IContatoRepository
{
    public async Task<Contato?> ObterPorIdAsync(Guid contatoId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Contatos
            .FirstOrDefaultAsync(c => c.Id == contatoId, cancellationToken);
    }

    public void Excluir(Contato contato)
    {
        dbContext.Contatos.Remove(contato);
    }
}
