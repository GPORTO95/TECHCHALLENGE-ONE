﻿using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Application.Abstractions.Messaging;
using Fiap.TechChallenge.Atualizacao.API.Events;
using Fiap.TechChallenge.Atualizacao.Repositories;
using Fiap.TechChallenge.Kernel;
using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;

namespace Fiap.TechChallenge.Atualizacao.API.Commands;

internal sealed class AtualizarContatoCommandHandler(
    IContatoRepository contatoRepository,
    IDddRepository dddRepository,
    IEventBus bus)
    : ICommandHandler<AtualizarContatoCommand>
{
    public async Task<Result> Handle(AtualizarContatoCommand request, CancellationToken cancellationToken)
    {
        Contato? contato = await contatoRepository.ObterPorIdAsync(request.ContatoId, cancellationToken);

        if (contato is null)
        {
            return Result.Failure(ContatoErrors.NaoEncontrado(request.ContatoId));
        }

        if (request.Email != contato.Email.Value)
        {
            Result<Email> emailResult = Email.Criar(request.Email);

            if (emailResult.IsFailure)
            {
                return Result.Failure<Guid>(emailResult.Error);
            }

            contato.AtualizarEmail(emailResult.Value);
        }

        if (request.Nome != contato.Nome.Value)
        {
            Result<Nome> nomeResult = Nome.Criar(request.Nome);

            if (nomeResult.IsFailure)
            {
                return Result.Failure<Guid>(nomeResult.Error);
            }

            contato.AtualizarNome(nomeResult.Value);
        }

        if (request.Telefone != contato.Telefone.Value)
        {
            Result<Telefone> telefoneResult = Telefone.Criar(request.Telefone);

            if (telefoneResult.IsFailure)
            {
                return Result.Failure<Guid>(telefoneResult.Error);
            }

            contato.AtualizarTelefone(telefoneResult.Value);
        }

        Result<Codigo> codigoRegiaoResult = Codigo.Criar(request.Ddd);

        if (codigoRegiaoResult.IsFailure)
        {
            return Result.Failure<Guid>(codigoRegiaoResult.Error);
        }

        Guid dddId = await dddRepository.ObterPorCodigoAsync(codigoRegiaoResult.Value, cancellationToken);

        if (dddId == Guid.Empty)
        {
            return Result.Failure<Guid>(DddErrors.CodigoNaoEncontrado(request.Ddd));
        }

        if (dddId != contato.DddId)
        {
            contato.AtualizarDdd(dddId);
        }

        await bus.PublishAsync(new ContatoAtualizadoEvent(contato), cancellationToken);

        return Result.Success();
    }
}
