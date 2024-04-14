using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Domain.Ddds;

public sealed record Estado
{
    private const int TAMANHA_SIGLA_ESTADO = 2;

    private Estado(string sigla, string descricao)
    {
        Sigla = sigla;
        Descricao = descricao;
    }

    public string Sigla { get; }
    public string Descricao { get; }

    public static Result<Estado> Criar(string sigla, string descricao)
    {
        if (string.IsNullOrWhiteSpace(sigla))
        {
            return Result.Failure<Estado>(EstadoErrors.Vazio("Sigla"));
        }

        if (string.IsNullOrWhiteSpace(descricao))
        {
            return Result.Failure<Estado>(EstadoErrors.Vazio("Descrição"));
        }

        if (sigla.Length != TAMANHA_SIGLA_ESTADO)
        {
            return Result.Failure<Estado>(EstadoErrors.TamanhoInvalido);
        }

        return new Estado(sigla, descricao);
    }
}

public static class EstadoErrors
{
    public static Error Vazio(string propriedade) => Error.Problem("Estado.Vazio", $"A {propriedade} do estado está vázio");

    public static readonly Error TamanhoInvalido = Error.Problem("Estado.TamanhoInvalido", "A sigla está inválido");
}
