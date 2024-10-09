using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Application.Abstractions.Messaging;
using Fiap.TechChallenge.Exclusao.API.Events;
using Fiap.TechChallenge.Exclusao.API.Repositories;
using Fiap.TechChallenge.Kernel;
using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Exclusao.API.Commands;

internal sealed class ExcluirContatoCommandHandler(
    IContatoRepository contatoRepository,
    IEventBus bus) : ICommandHandler<ExcluirContatoCommand>
{
    public async Task<Result> Handle(ExcluirContatoCommand request, CancellationToken cancellationToken)
    {
        Contato? contato = await contatoRepository.ObterPorIdAsync(request.ContatoId, cancellationToken);

        if (contato is null)
        {
            return Result.Failure(ContatoErrors.NaoEncontrado(request.ContatoId));
        }

        await bus.PublishAsync(new ContatoExcluidoEvent(contato), cancellationToken);

        return Result.Success();
    }
}
