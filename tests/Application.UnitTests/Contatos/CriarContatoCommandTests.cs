using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Contatos.Criar;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Ddds;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Contatos;

public class CriarContatoCommandTests
{
    private static readonly CriarContatoCommand Command = new("test@test.com", "Gabriel Test", "987654321", Guid.NewGuid());
    
    private readonly CriarContatoCommandHandler _handler;
    private readonly IContatoRepository _contatoRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IDddRepository _dddRepositoryMock;

    public CriarContatoCommandTests()
    {
        _contatoRepositoryMock = Substitute.For<IContatoRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _dddRepositoryMock = Substitute.For<IDddRepository>();

        _handler = new(_contatoRepositoryMock, _unitOfWorkMock, _dddRepositoryMock);
    }

    [Fact]
    public async Task Handle_Deve_RetornarSucesso_QuandoTudoEhValido()
    {
        // Arrange
        _dddRepositoryMock.ExisteAsync(Arg.Is<Guid>(e => e == Command.DddId), Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        _contatoRepositoryMock.Received(1).Adicionar(Arg.Is<Contato>(u => u.Id == result.Value));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoEmailEhInvalido()
    {
        // Arrange
        CriarContatoCommand emailInvalidoCommand = Command with
        {
            Email = "teste@test"
        };

        // Act
        var result = await _handler.Handle(emailInvalidoCommand, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmailErrors.FormatoInvalido);
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoNomeEhInvalido()
    {
        // Arrange
        CriarContatoCommand nomeInvalidoCommand = Command with
        {
            Nome = "Gabriel T3ste"
        };

        // Act
        var result = await _handler.Handle(nomeInvalidoCommand, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(NomeErrors.FormatoInvalido);
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoTelefoneEhInvalido()
    {
        // Arrange
        CriarContatoCommand telefoneInvalido = Command with
        {
            Telefone = "9765490A1"
        };

        // Act
        var result = await _handler.Handle(telefoneInvalido, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TelefoneErrors.FormatoInvalido);
    }

    [Fact]
    public async Task Handle_Deve_RetornarErro_QuandoDddNaoExiste()
    {
        // Arrange
        _dddRepositoryMock.ExisteAsync(Arg.Is<Guid>(e => e == Command.DddId), Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DddErrors.NaoEncontrado(Command.DddId));
    }
}
