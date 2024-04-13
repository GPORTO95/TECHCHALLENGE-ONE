using Fiap.TechChallenge.One.Domain.Contatos;

namespace Fiap.TechChallenge.One.Application.Contatos;

public sealed record ContatoResponse(
    Guid Id,
    string Nome,
    string Email,
    string Telefone)
{
    public static ContatoResponse ContatoParaContatoResponse(Contato contato)
    {
        return new ContatoResponse(
            contato.Id,
            contato.Nome.Value,
            contato.Email.Value,
            contato.Telefone.Value);
    }
};