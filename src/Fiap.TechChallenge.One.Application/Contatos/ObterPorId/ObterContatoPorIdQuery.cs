using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.One.Application.Contatos.ObterPorId;

public sealed record ObterContatoPorIdQuery(Guid ContatoId) : IQuery<ContatoResponse>;
