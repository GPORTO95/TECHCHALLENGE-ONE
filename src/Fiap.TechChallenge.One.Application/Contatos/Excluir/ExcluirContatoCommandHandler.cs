using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Abstractions.Messaging;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Application.Contatos.Excluir;

internal sealed class ExcluirContatoCommandHandler(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ExcluirContatoCommand>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ExcluirContatoCommand request, CancellationToken cancellationToken)
    {
        Contato? contato = await _contatoRepository.ObterPorIdAsync(request.ContatoId, cancellationToken);

        if (contato is null)
        {
            return Result.Failure(ContatoErrors.NaoEncontrado(request.ContatoId));
        }

        _contatoRepository.Excluir(contato);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
