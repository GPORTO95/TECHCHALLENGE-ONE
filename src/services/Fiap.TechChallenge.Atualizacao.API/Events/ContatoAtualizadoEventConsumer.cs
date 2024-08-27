using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Atualizacao.Repositories;
using MassTransit;

namespace Fiap.TechChallenge.Atualizacao.API.Events;

internal sealed class ContatoAtualizadoEventConsumer(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork,
    ILogger<ContatoAtualizadoEventConsumer> logger) : IConsumer<ContatoAtualizadoEvent>
{
    public async Task Consume(ConsumeContext<ContatoAtualizadoEvent> context)
    {
        logger.LogInformation("Mensagem recebida de atualização de Contato");

        contatoRepository.Atualizar(context.Message.Contato);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
