namespace Fiap.TechChallenge.Kernel.Ddds;

public interface IDddRepository
{
    Task<bool> ExisteAsync(Guid dddId, CancellationToken cancellationToken = default);

    Task<Guid> ObterPorCodigoAsync(Codigo codigo, CancellationToken cancellationToken = default);
}
