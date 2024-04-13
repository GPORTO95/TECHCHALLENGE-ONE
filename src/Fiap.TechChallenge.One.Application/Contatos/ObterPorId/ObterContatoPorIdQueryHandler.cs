using Fiap.TechChallenge.One.Application.Abstractions.Messaging;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Application.Contatos.ObterPorId;

internal sealed class ObterContatoPorIdQueryHandler(IContatoRepository contatoRepository)
    : IQueryHandler<ObterContatoPorIdQuery, ContatoResponse>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;

    public async Task<Result<ContatoResponse>> Handle(ObterContatoPorIdQuery request, CancellationToken cancellationToken)
    {
        Contato? contato = await _contatoRepository.ObterPorIdAsync(
            request.ContatoId, cancellationToken);

        if (contato is null)
        {
            return Result.Failure<ContatoResponse>(ContatoErrors.NaoEncontrado(request.ContatoId));
        }

        return ContatoResponse.ContatoParaContatoResponse(contato);
    }
}
