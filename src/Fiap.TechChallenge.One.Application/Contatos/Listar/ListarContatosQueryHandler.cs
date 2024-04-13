using Fiap.TechChallenge.One.Application.Abstractions.Messaging;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Application.Contatos.Listar;

internal sealed class ListarContatosQueryHandler(IContatoRepository contatoRepository)
    : IQueryHandler<ListarContatosQuery, IEnumerable<ContatoResponse>>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;

    public async Task<Result<IEnumerable<ContatoResponse>>> Handle(ListarContatosQuery request, CancellationToken cancellationToken)
    {
        List<Contato> contatos = await _contatoRepository.ListarAsync(cancellationToken);

        if (!contatos.Any())
        {
            return Array.Empty<ContatoResponse>();
        }

        List<ContatoResponse> response = new(contatos.Count);

        foreach (Contato contato in contatos)
        {
            response.Add(ContatoResponse.ContatoParaContatoResponse(contato));
        }

        return response;
    }
}
