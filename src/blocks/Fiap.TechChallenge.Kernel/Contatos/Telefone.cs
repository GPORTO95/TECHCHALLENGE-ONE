using System.Text.RegularExpressions;

namespace Fiap.TechChallenge.Kernel.Contatos;

public sealed record Telefone
{
    public const int Length = 9;

    public Telefone() { }
    
    private Telefone(string value) => Value = value;

    public string Value { get; set; }

    public static Result<Telefone> Criar(string? telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
        {
            return Result.Failure<Telefone>(TelefoneErrors.Vazio);
        }

        if (telefone.Length != Length)
        {
            return Result.Failure<Telefone>(TelefoneErrors.TamanhoInvalido);
        }

        if (!Regex.IsMatch(telefone, @"\b\d{9}\b"))
        {
            return Result.Failure<Telefone>(TelefoneErrors.FormatoInvalido);
        }

        return new Telefone(telefone);
    }
}

public static class TelefoneErrors
{
    public static readonly Error Vazio = Error.Problem("Telefone.Vazio", "Email está vázio");

    public static readonly Error TamanhoInvalido = Error.Problem("Telefone.Tamanho", "O tamanho do telefone está inválido, deve ser fornecido como 9########");

    public static readonly Error FormatoInvalido = Error.Problem("Telefone.FormatoInvalido", "Formato inválido, deve ser fornecido 9########");
}