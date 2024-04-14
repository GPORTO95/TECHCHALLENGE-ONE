namespace Fiap.TechChallenge.One.Domain.Ddds;

public interface IDddRepository
{
    Task<bool> ExisteAsync(Guid dddId, CancellationToken cancellationToken = default);
}
