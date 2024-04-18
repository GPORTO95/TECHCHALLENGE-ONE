using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Domain.Ddds;

public sealed class Ddd : Entity
{
    private readonly List<Contato> _contatos = [];

    protected Ddd() { }

    private Ddd(
        Guid id, Codigo codigoRegiao, Estado estado)
    {
        Id = id;
        CodigoRegiao = codigoRegiao;
        Estado = estado;
    }

    /// <summary>
    /// Objeto de valor para preenchimento do Código da Região
    /// </summary>
    /// <example>111</example>
    public Codigo CodigoRegiao { get; private set; }

    /// <summary>
    /// Objeto de valor para preenchimento da sigla e descrição de estado
    /// </summary>
    /// <param name="Sigla">Deve ser informado a sigla 'SP'</param>
    /// <param name="Descrição">Deve ser informado a descrição da sigla 'São Paulo'</param>
    public Estado Estado { get; private set; }

    public IReadOnlyList<Contato> Contatos => _contatos.AsReadOnly();

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
