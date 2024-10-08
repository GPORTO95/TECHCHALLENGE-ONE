﻿using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Application.Abstractions.Messaging;
using Fiap.TechChallenge.Cadastro.API.Events;
using Fiap.TechChallenge.Kernel;
using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;

namespace Fiap.TechChallenge.Cadastro.API.Commands;

internal sealed class CriarContatoCommandHandler(IDddRepository dddRepository, IEventBus bus) : ICommandHandler<CriarContatoCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CriarContatoCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Criar(request.Email);

        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        Result<Nome> nomeResult = Nome.Criar(request.Nome);

        if (nomeResult.IsFailure)
        {
            return Result.Failure<Guid>(nomeResult.Error);
        }

        Result<Telefone> telefoneResult = Telefone.Criar(request.Telefone);

        if (telefoneResult.IsFailure)
        {
            return Result.Failure<Guid>(telefoneResult.Error);
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

        Result<Contato> contatoResult =
            Contato.Criar(
                nomeResult.Value,
                emailResult.Value,
                telefoneResult.Value,
                dddId);

        Guid contatoId = Guid.NewGuid();

        await bus.PublishAsync(new ContatoInseridoEvent(contatoResult.Value), cancellationToken);

        return contatoId;
    }
}
