using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Atualizacao.API.Commands;
using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;
using FluentAssertions;
using NSubstitute;
using Fiap.TechChallenge.Atualizacao.Repositories;
using Fiap.TechChallenge.Atualizacao.API.Events;

namespace Fiap.TechChallenge.Cadastro.UnitTests;

public class AtualizarContatoCommandTests
{
    private static readonly Contato Contato = Contato.Criar(
        Nome.Criar("Gabriel Teste").Value,
        Email.Criar("gabriel.test@test.com").Value,
        Telefone.Criar("987654321").Value,
        Guid.NewGuid()).Value;

    private static readonly AtualizarContatoCommand Command = new(
        Guid.NewGuid(),
        "gabriel.test@test.com",
        "Gabriel Teste",
        "987654321",
        "11");

    private readonly AtualizarContatoCommandHandler _handler;
    private readonly IContatoRepository _contatoRepositoryMock;
    private readonly IDddRepository _dddRepositoryMock;
    private readonly IEventBus _busMock;

    public AtualizarContatoCommandTests()
    {
        _contatoRepositoryMock = Substitute.For<IContatoRepository>();
        _busMock = Substitute.For<IEventBus>();
        _dddRepositoryMock = Substitute.For<IDddRepository>();

        _handler = new(_contatoRepositoryMock, _dddRepositoryMock, _busMock);
    }

    [Fact]
    public async Task Handle_Deve_RetornarSucesso_QuandoTudoEhValido()
    {
        // Arrange
        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == Command.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        _dddRepositoryMock.ObterPorCodigoAsync(
            Arg.Is<Codigo>(e => e == Codigo.Criar(Command.Ddd).Value), Arg.Any<CancellationToken>())
            .Returns(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();

        await _busMock.Received(1).PublishAsync(
            Arg.Any<ContatoAtualizadoEvent>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoContatoNaoExiste()
    {
        // Arrange
        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == Command.ContatoId), Arg.Any<CancellationToken>())
            .Returns((Contato?)null);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ContatoErrors.NaoEncontrado(Command.ContatoId));
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoEmailEhDiferenteEInvalido()
    {
        // Arrange
        AtualizarContatoCommand commandInvalido = Command with
        {
            Email = ""
        };

        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == Command.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        // Act
        var result = await _handler.Handle(commandInvalido, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmailErrors.Vazio);
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoNomeEhDiferenteEInvalido()
    {
        // Arrange
        AtualizarContatoCommand commandInvalido = Command with
        {
            Nome = ""
        };

        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == Command.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        // Act
        var result = await _handler.Handle(commandInvalido, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(NomeErrors.Vazio);
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoTelefoneEhDiferenteEInvalido()
    {
        // Arrange
        AtualizarContatoCommand commandInvalido = Command with
        {
            Telefone = "98765345F"
        };

        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == Command.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        // Act
        var result = await _handler.Handle(commandInvalido, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TelefoneErrors.FormatoInvalido);
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoDddEhInvalido()
    {
        // Arrange
        AtualizarContatoCommand commandInvalido = Command with
        {
            Ddd = "1A"
        };

        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == Command.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        // Act
        var result = await _handler.Handle(commandInvalido, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CodigoErrors.ValorInvalido);
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoDddNaoExiste()
    {
        // Arrange
        AtualizarContatoCommand commandInvalido = Command with
        {
            Ddd = "19"
        };

        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == commandInvalido.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        _dddRepositoryMock.ObterPorCodigoAsync(
            Arg.Is<Codigo>(e => e == Codigo.Criar(commandInvalido.Ddd).Value), Arg.Any<CancellationToken>())
            .Returns(Guid.Empty);

        // Act
        var result = await _handler.Handle(commandInvalido, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DddErrors.CodigoNaoEncontrado(commandInvalido.Ddd));
    }
}
