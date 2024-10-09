using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Exclusao.API.Commands;
using Fiap.TechChallenge.Exclusao.API.Events;
using Fiap.TechChallenge.Exclusao.API.Repositories;
using Fiap.TechChallenge.Kernel;
using Fiap.TechChallenge.Kernel.Contatos;
using FluentAssertions;
using NSubstitute;

namespace Fiap.TechChallenge.Exclusao.UnitTests;

public class ExcluirContatoCommandTests
{
    private readonly static ExcluirContatoCommand Command = new(Guid.NewGuid());

    private readonly ExcluirContatoCommandHandler _handler;
    private readonly IContatoRepository _contatoRepositoryMock;
    private readonly IEventBus _busMock;

    public ExcluirContatoCommandTests()
    {
        _contatoRepositoryMock = Substitute.For<IContatoRepository>();
        _busMock = Substitute.For<IEventBus>();

        _handler = new(_contatoRepositoryMock, _busMock);
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoContatoNaoExistir()
    {
        // Arrange
        _contatoRepositoryMock.ObterPorIdAsync(
            Command.ContatoId, Arg.Any<CancellationToken>())
            .Returns((Contato?)null);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ContatoErrors.NaoEncontrado(Command.ContatoId));

    }

    [Fact]
    public async Task Handle_DeveRetornarSucesso_QuandoContatoExistir()
    {
        // Arrange
        Contato contato = Contato.Criar(
            Nome.Criar("Gabriel Teste").Value,
            Email.Criar("gabriel.test@test.com").Value,
            Telefone.Criar("987654321").Value,
            Guid.NewGuid()).Value;

        _contatoRepositoryMock.ObterPorIdAsync(
            Command.ContatoId, Arg.Any<CancellationToken>())
            .Returns(contato);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _busMock.Received(1).PublishAsync(
            Arg.Any<ContatoExcluidoEvent>(),
            Arg.Any<CancellationToken>());
    }
}
