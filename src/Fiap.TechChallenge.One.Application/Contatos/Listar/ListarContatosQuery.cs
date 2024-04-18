using Fiap.TechChallenge.One.Application.Abstractions.Messaging;

namespace Fiap.TechChallenge.One.Application.Contatos.Listar;

public sealed record ListarContatosQuery(string? Ddd) : IQuery<IEnumerable<ContatoResponse>>;
