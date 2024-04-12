namespace Fiap.TechChallenge.One.Domain.Contatos;

public interface IContatoRepository
{
    Task<List<Contato>> ListarAsync(CancellationToken cancellationToken = default);

    void Adicionar(Contato contato);

    void Atualizar(Contato contato);

    void Excluir(Contato contato);
}
