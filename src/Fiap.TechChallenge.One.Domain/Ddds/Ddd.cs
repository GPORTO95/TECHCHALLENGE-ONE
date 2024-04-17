using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Domain.Ddds;

public sealed class Ddd : Entity
{
    protected Ddd() { }

    private Ddd(
        Guid id, Codigo codigoRegiao, Estado estado)
    {
        Id = id;
        CodigoRegiao = codigoRegiao;
        Estado = estado;
    }

    
    public Codigo CodigoRegiao { get; private set; }

    public Estado Estado { get; private set; }

    public static Ddd Criar(
        Codigo codigoRegiao, Estado estado)
    {
        return new Ddd(
            Guid.NewGuid(),
            codigoRegiao,
            estado);
    }
}
public static class DddErrors
{
    public static Error NaoEncontrado(Guid id) => Error.Problem("Ddd.NaoEncontrado", $"Ddd não encontrado para o Id = '{id}' informado");

    public static Error CodigoNaoEncontrado(string ddd) => Error.Problem("Ddd.NaoEncontrado", $"Ddd não encontrado para o Valor = '{ddd}' informado");
}
