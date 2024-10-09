using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Cadastro.API.Repositories;

public interface IContatoRepository
{
    void Adicionar(Contato contato);
}