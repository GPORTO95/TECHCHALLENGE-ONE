using System.Text.RegularExpressions;

namespace Fiap.TechChallenge.Kernel.Contatos;

public sealed record Nome
{
    public Nome() { }

    private Nome(string value) => Value = value;

    public string Value { get; set; }

    public static Result<Nome> Criar(string? nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return Result.Failure<Nome>(NomeErrors.Vazio);
        }

        var nomeSplit = nome.Trim().Split(" ");

        if (nomeSplit.Length <= 1)
        {
            return Result.Failure<Nome>(NomeErrors.NomeIncompleto);
        }

        if (!Regex.IsMatch(nome, @"^[a-zA-ZÀ-ÿ\s]+$"))
        {
            return Result.Failure<Nome>(NomeErrors.FormatoInvalido);
        }

        return new Nome(nome);
    }
}

public static class NomeErrors
{
    public static readonly Error Vazio = Error.Problem("Nome.Vazio", "Nome está vázio");

    public static readonly Error NomeIncompleto = Error.Problem("Nome.NomeIncompleto", "Informe o nome completo");

    public static readonly Error FormatoInvalido = Error.Problem("Nome.FormatoInvalido", "Nome com formato inválido");
}
