using Fiap.TechChallenge.One.Domain.Ddds;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Domain.Contatos;

public sealed class Contato : Entity
{
    protected Contato() { }

    private Contato(
        Guid id,
        Nome nome,
        Email email,
        Telefone telefone,
        Guid dddId)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;

        DddId = dddId;
    }

    /// <summary>
    /// Objeto de valor para preenchimento de Nome
    /// </summary>
    /// <example>Gabriel Test</example>
    public Nome Nome { get; private set; }

    /// <summary>
    /// Objeto de valor para preenchimento de Email
    /// </summary>
    /// <example>gabriel@test.com</example>
    public Email Email { get; private set; }

    /// <summary>
    /// Objeto de valor para preenchimento de Telene
    /// </summary>
    /// <example>989765432</example>
    public Telefone Telefone { get; private set; }

    /// <summary>
    /// Guid com referencia para Id da tabela de Ddds
    /// </summary>
    public Guid DddId { get; private set; }

    /// <summary>
    /// Relacionamento com entidade de Ddd
    /// </summary>
    public Ddd Ddd { get; private set; }

    public static Result<Contato> Criar(
        Nome nome, Email email, Telefone telefone, Guid dddId)
    {
        return new Contato(
            Guid.NewGuid(),
            nome,
            email,
            telefone,
            dddId);
    }

    public void AtualizarDdd(Guid dddId) =>
        DddId = dddId;

    public void AtualizarEmail(Email email) =>
        Email = email;

    public void AtualizarNome(Nome nome) =>
        Nome = nome;

    public void AtualizarTelefone(Telefone telefone) =>
        Telefone = telefone;
}
