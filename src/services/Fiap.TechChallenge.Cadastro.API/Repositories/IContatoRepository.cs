using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Cadastro.Repositories;

public interface IContatoRepository
{
    void Adicionar(Contato contato);
}