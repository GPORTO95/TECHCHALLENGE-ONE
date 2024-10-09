using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;

namespace Fiap.TechChallenge.Listagem.API.Repositories;

public interface IContatoRepository
{
    Task<List<Contato>> ListarAsync(Codigo? codigo, CancellationToken cancellationToken = default);
}