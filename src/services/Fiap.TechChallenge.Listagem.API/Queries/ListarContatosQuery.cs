using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.Listagem.API.Application.Contatos.Listar;

public sealed record ListarContatosQuery(string? Ddd) : IQuery<IEnumerable<ContatoResponse>>;

public sealed record ContatoResponse(
    Guid ContatoId,
    string Nome,
    string Email,
    string Telefone,
    string Ddd)
{
    public static ContatoResponse ContatoParaContatoResponse(Contato contato)
    {
        return new ContatoResponse(
            contato.Id,
            contato.Nome.Value,
            contato.Email.Value,
            contato.Telefone.Value,
            contato.Ddd?.CodigoRegiao?.Valor);
    }
};