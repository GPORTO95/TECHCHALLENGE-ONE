using Fiap.TechChallenge.Kernel;

namespace Fiap.TechChallenge.Kernel.Contatos;

public static class ContatoErrors
{
    public static Error NaoEncontrado(Guid contatoId) => Error.NotFound(
        "Contatos.NaoEncontrado",
        $"O contato com Id = '{contatoId}' não foi encontrado");
}