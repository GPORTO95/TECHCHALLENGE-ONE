using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.One.Application.Contatos.Excluir;

public sealed record ExcluirContatoCommand(Guid ContatoId) : ICommand;
