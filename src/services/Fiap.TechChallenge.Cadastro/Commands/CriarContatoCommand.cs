using Fiap.TechChallenge.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.Cadastro.Commands;

public sealed record CriarContatoCommand(
    string Email,
    string Nome,
    string Telefone,
    string Ddd) : ICommand<Guid>;
