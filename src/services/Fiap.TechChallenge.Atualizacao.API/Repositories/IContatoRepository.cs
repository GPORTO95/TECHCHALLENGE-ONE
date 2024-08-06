using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Atualizacao.Repositories;

public interface IContatoRepository
{
    Task<Contato?> ObterPorIdAsync(Guid contatoId, CancellationToken cancellationToken = default);
    void Atualizar(Contato contato);

}