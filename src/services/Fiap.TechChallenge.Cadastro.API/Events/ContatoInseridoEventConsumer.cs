using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Cadastro.API.Repositories;
using MassTransit;

namespace Fiap.TechChallenge.Cadastro.API.Events;

internal sealed class ContatoInseridoEventConsumer(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork) : IConsumer<ContatoInseridoEvent>
{
    public async Task Consume(ConsumeContext<ContatoInseridoEvent> context)
    {
        contatoRepository.Adicionar(context.Message.Contato);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
