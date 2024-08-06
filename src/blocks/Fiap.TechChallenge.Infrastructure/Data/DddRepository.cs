using Fiap.TechChallenge.Kernel.Ddds;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TechChallenge.Infrastructure.Data;

public sealed class DddRepository(ApplicationDbContext dbContext) : IDddRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<bool> ExisteAsync(Guid dddId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ddds
            .AnyAsync(d => d.Id == dddId, cancellationToken);
    }

    public async Task<Guid> ObterPorCodigoAsync(Codigo codigo, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ddds
            .Where(d => d.CodigoRegiao == codigo)
            .Select(d => d.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
