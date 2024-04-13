using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Domain.Contatos;

public sealed class Contato : Entity
{
    private Contato(
        Guid id,
        Nome nome,
        Email email,
        Telefone telefone)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public Nome Nome { get; private set; }

    public Email Email { get; private set; }

    public Telefone Telefone { get; private set; }

    public static Result<Contato> Criar(
        Nome nome, Email email, Telefone telefone)
    {
        return new Contato(
            Guid.NewGuid(),
            nome,
            email,
            telefone);
    }

    public void AtualizarEmail(Email email) =>
        Email = email;

    public void AtualizarNome(Nome nome) =>
        Nome = nome;

    public void AtualizarTelefone(Telefone telefone) =>
        Telefone = telefone;
}
