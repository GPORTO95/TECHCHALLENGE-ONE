﻿using Fiap.TechChallenge.Kernel.Contatos;

namespace Fiap.TechChallenge.Cadastro.API.Events;

public sealed record ContatoInseridoEvent(
    Contato Contato);
