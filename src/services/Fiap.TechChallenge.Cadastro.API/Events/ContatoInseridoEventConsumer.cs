using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Cadastro.Repositories;
using Fiap.TechChallenge.Kernel.Contatos;
using MassTransit;

namespace Fiap.TechChallenge.Cadastro.Events;

internal sealed class ContatoInseridoEventConsumer(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork) : IConsumer<ContatoInseridoEvent>
{
    public async Task Consume(ConsumeContext<ContatoInseridoEvent> context)
    {
        contatoRepository.Adicionar(
            new Contato(
                context.Message.ContatoId,
                context.Message.Nome,
                context.Message.Email,
                context.Message.Telefone,
                context.Message.DddId));

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
