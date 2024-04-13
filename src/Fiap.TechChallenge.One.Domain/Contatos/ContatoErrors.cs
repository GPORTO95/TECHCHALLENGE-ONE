using Fiap.TechChallenge.One.Domain.Kernel;

namespace Fiap.TechChallenge.One.Domain.Contatos;

public static class ContatoErrors
{
    public static Error NaoEncontrado(Guid contatoId) => Error.NotFound(
        "Contatos.NaoEncontrado",
        $"O contato com Id = '{contatoId}' não foi encontrado");
}