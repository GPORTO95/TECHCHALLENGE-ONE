using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Abstractions.Messaging;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Application.Contatos.Criar;

internal sealed class CriarUsuarioCommandHandler(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CriarUsuarioCommand, Guid>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
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

        Result<Contato> contatoResult = Contato.Criar(
            nomeResult.Value,
            emailResult.Value,
            telefoneResult.Value);

        _contatoRepository.Adicionar(contatoResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return contatoResult.Value.Id;
    }
}
