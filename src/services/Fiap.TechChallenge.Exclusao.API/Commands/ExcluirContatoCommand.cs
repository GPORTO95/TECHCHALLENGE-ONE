using Fiap.TechChallenge.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.Exclusao.API.Commands;

public sealed record ExcluirContatoCommand(Guid ContatoId): ICommand;
