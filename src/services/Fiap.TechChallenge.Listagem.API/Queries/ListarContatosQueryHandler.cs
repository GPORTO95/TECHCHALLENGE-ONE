using Fiap.TechChallenge.Kernel;
using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;
using Fiap.TechChallenge.Listagem.API.Repositories;
using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.Listagem.API.Application.Contatos.Listar;

internal sealed class ListarContatosQueryHandler(IContatoRepository contatoRepository)
    : IQueryHandler<ListarContatosQuery, IEnumerable<ContatoResponse>>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;

    public async Task<Result<IEnumerable<ContatoResponse>>> Handle(ListarContatosQuery request, CancellationToken cancellationToken)
    {
        Result<Codigo>? codigoResult = null;

        if (!string.IsNullOrWhiteSpace(request.Ddd))
        {
            codigoResult = Codigo.Criar(request.Ddd);

            if (codigoResult.IsFailure)
            {
                return Result.Failure<IEnumerable<ContatoResponse>>(codigoResult.Error);
            }
        }

        List<Contato> contatos = await _contatoRepository.ListarAsync(codigoResult?.Value, cancellationToken);

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
