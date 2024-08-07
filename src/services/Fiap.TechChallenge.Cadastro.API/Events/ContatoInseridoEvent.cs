using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Cadastro.Events;

public sealed record ContatoInseridoEvent(
    Contato Contato);
