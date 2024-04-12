using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.One.Application.Contatos.Criar;

public sealed record CriarUsuarioCommand(
    string Email,
    string Nome,
    string Telefone) : ICommand<Guid>;
