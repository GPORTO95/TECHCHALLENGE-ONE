using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Cadastro.API.Repositories;
using MassTransit;

namespace Fiap.TechChallenge.Cadastro.API.Events;

internal sealed class ContatoInseridoEventConsumer(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork,
    ILogger<ContatoInseridoEventConsumer> logger) : IConsumer<ContatoInseridoEvent>
{
    public async Task Consume(ConsumeContext<ContatoInseridoEvent> context)
    {
        logger.LogInformation("Mensagem recebida de inserção de Contato");

        contatoRepository.Adicionar(context.Message.Contato);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
