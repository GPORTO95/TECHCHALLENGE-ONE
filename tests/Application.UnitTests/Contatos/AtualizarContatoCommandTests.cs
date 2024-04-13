using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Contatos.Atualizar;
using Fiap.TechChallenge.One.Domain.Contatos;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Contatos;

public class AtualizarContatoCommandTests
{
    private static readonly Contato Contato = Contato.Criar(
        Nome.Criar("Gabriel Teste").Value,
        Email.Criar("gabriel.test@test.com").Value,
        Telefone.Criar("987654321").Value).Value;

    private static readonly AtualizarContatoCommand Command = new(
        Guid.NewGuid(),
        "gabriel.test@test.com",
        "Gabriel Teste",
        "987654321");

    private readonly AtualizarContatoCommandHandler _handler;
    private readonly IContatoRepository _contatoRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public AtualizarContatoCommandTests()
    {
        _contatoRepositoryMock = Substitute.For<IContatoRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new(_contatoRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Deve_RetornarSucesso_QuandoTudoEhValido()
    {
        // Arrange
        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(e => e == Command.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _contatoRepositoryMock.Received(1).Atualizar(Arg.Is<Contato>(u => u.Id == Contato.Id));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
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
}
