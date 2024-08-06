using Fiap.TechChallenge.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.Atualizacao.API.Commands;

public sealed record AtualizarContatoCommand(
    Guid ContatoId,
    string Email,
    string Nome,
    string Telefone,
    string Ddd) : ICommand;