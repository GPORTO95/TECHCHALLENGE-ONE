using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.One.Application.Contatos.Atualizar;

public sealed record AtualizarContatoCommand(
    Guid ContatoId,
    string Email,
    string Nome,
    string Telefone) : ICommand;
