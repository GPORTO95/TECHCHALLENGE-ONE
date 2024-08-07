using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Exclusao.API.Repositories;
using MassTransit;

namespace Fiap.TechChallenge.Exclusao.API.Events;

public class ContatoExcluidoEventConsumer(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork) : IConsumer<ContatoExcluidoEvent>
{
    public async Task Consume(ConsumeContext<ContatoExcluidoEvent> context)
    {
        contatoRepository.Excluir(context.Message.Contato);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
