using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Atualizacao.Repositories;
using MassTransit;

namespace Fiap.TechChallenge.Atualizacao.API.Events;

internal sealed class ContatoAtualizadoEventConsumer(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork) : IConsumer<ContatoAtualizadoEvent>
{
    public async Task Consume(ConsumeContext<ContatoAtualizadoEvent> context)
    {
        contatoRepository.Atualizar(context.Message.Contato);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
