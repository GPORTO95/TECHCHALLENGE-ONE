﻿using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Abstractions.Messaging;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Ddds;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Application.Contatos.Atualizar;

internal sealed class AtualizarContatoCommandHandler(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork,
    IDddRepository dddRepository)
    : ICommandHandler<AtualizarContatoCommand>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IDddRepository _dddRepository = dddRepository;

    public async Task<Result> Handle(AtualizarContatoCommand request, CancellationToken cancellationToken)
    {
        Contato? contato = await _contatoRepository.ObterPorIdAsync(request.ContatoId, cancellationToken);

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

        Guid dddId = await _dddRepository.ObterPorCodigoAsync(codigoRegiaoResult.Value, cancellationToken);

        if (dddId == Guid.Empty)
        {
            return Result.Failure<Guid>(DddErrors.CodigoNaoEncontrado(request.Ddd));
        }

        if (dddId != contato.DddId)
        {
            contato.AtualizarDdd(dddId);
        }

        _contatoRepository.Atualizar(contato);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
