using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Exclusao.API.Repositories;

public interface IContatoRepository
{
    Task<Contato?> ObterPorIdAsync(Guid contatoId, CancellationToken cancellationToken = default);

    void Excluir(Contato contato);
}
