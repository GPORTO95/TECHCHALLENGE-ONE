using Fiap.TechChallenge.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.Cadastro.API.Commands;

public sealed record CriarContatoCommand(
    string Email,
    string Nome,
    string Telefone,
    string Ddd) : ICommand<Guid>;
