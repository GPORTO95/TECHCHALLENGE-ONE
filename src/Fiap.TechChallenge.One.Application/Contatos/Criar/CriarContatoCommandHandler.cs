using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Abstractions.Messaging;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Ddds;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Application.Contatos.Criar;

internal sealed class CriarContatoCommandHandler(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork,
    IDddRepository dddRepository)
    : ICommandHandler<CriarContatoCommand, Guid>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IDddRepository _dddRepository = dddRepository;

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

        if (!await _dddRepository.ExisteAsync(request.DddId, cancellationToken))
        {
            return Result.Failure<Guid>(DddErrors.NaoEncontrado(request.DddId));
        }

        Result<Contato> contatoResult = Contato.Criar(
            nomeResult.Value,
            emailResult.Value,
            telefoneResult.Value,
            request.DddId);

        _contatoRepository.Adicionar(contatoResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return contatoResult.Value.Id;
    }
}
