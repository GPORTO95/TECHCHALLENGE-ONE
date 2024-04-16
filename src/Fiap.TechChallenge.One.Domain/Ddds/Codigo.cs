using Fiap.TechChallenge.One.Domain.Kernel;
using System.Text.RegularExpressions;

namespace Fiap.TechChallenge.One.Domain.Ddds;

public sealed record Codigo
{
    private const int TAMANHA_DDD = 2;

    private Codigo(string valor)
    {
        Valor = valor;
    }

    public string Valor { get; }

    public static Result<Codigo> Criar(
        string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return Result.Failure<Codigo>(CodigoErrors.Vazio);
        }

        if (valor.Length != TAMANHA_DDD)
        {
            return Result.Failure<Codigo>(CodigoErrors.TamanhoInvalido);
        }

        if (!Regex.IsMatch(valor, @"^\d{2}$"))
        {
            return Result.Failure<Codigo>(CodigoErrors.ValorInvalido);
        }

        return new Codigo(valor);
    }
}

public static class CodigoErrors 
{
    public static Error Vazio => Error.Problem("CodigoRegiao.Vazio", $"Código da região não informado");

    public static readonly Error TamanhoInvalido = Error.Problem("CodigoRegiao.TamanhoInvalido", "O tamanho informado não corresponde a um DDD");

    public static readonly Error ValorInvalido = Error.Problem("CodigoRegiao.ValorInvalido", "O valor informado para DDD não é valido");
}

