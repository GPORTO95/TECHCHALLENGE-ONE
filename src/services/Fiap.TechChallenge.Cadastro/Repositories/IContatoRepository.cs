using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Cadastro.Repositories;

public interface IContatoRepository
{
    void Adicionar(Contato contato);
}

internal sealed class ContatoRepository(ApplicationDbContext dbContext) : IContatoRepository
{
    public void Adicionar(Contato contato)
    {
        dbContext.Contatos.Add(contato);
    }
}