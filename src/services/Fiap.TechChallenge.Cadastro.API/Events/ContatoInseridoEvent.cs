using MediatR;

namespace Fiap.TechChallenge.Cadastro.Events;

public sealed record ContatoInseridoEvent(
    Guid ContatoId, string Nome, string Email, string Telefone, Guid DddId);
