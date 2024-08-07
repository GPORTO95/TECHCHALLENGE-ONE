using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Exclusao.API.Events;

public sealed record ContatoExcluidoEvent(Contato Contato);