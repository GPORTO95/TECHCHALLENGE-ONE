using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Domain.Ddds;

public sealed class Ddd : Entity
{
    private Ddd(
        Guid id, Estado estado)
    {
        Id = id;
        Estado = estado;
    }

    public Estado Estado { get; private set; }

    public static Ddd Criar(Estado estado)
    {
        return new Ddd(
            Guid.NewGuid(),
            estado);
    }
}
public static class DddErrors
{
    public static Error NaoEncontrado(Guid id) => Error.Problem("Ddd.NaoEncontrado", $"Ddd não encontrado para o Id = '{id}' informado");
}
