using Fiap.TechChallenge.One.Domain.Ddds;
using Fiap.TechChallenge.One.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TechChallenge.One.Infrastructure.Repositories;

internal sealed class DddRepository(ApplicationDbContext dbContext) : IDddRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<bool> ExisteAsync(Guid dddId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ddds
            .AnyAsync(d => d.Id == dddId, cancellationToken);
    }
}
