using System.Text.RegularExpressions;

namespace Fiap.TechChallenge.Kernel.Contatos;

public sealed record Email
{
    public Email() { }

    private Email(string value) => Value = value;

    public string Value { get; set; }

    public static Result<Email> Criar(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<Email>(EmailErrors.Vazio);
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            return Result.Failure<Email>(EmailErrors.FormatoInvalido);
        }

        return new Email(email);
    }
}

public static class EmailErrors
{
    public static readonly Error Vazio = Error.Problem("Email.Vazio", "Email está vázio");

    public static readonly Error FormatoInvalido = Error.Problem("Email.FormatoInvalido", "Email está inválido");
}