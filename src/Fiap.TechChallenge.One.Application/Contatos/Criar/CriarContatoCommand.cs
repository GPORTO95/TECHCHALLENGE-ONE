using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.One.Application.Contatos.Criar;

public sealed record CriarContatoCommand(
    string Email,
    string Nome,
    string Telefone,
    string Ddd) : ICommand<Guid>;
