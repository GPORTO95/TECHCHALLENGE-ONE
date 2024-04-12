using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Contatos.Criar;
using Fiap.TechChallenge.One.Domain.Contatos;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Contatos;

public class CriarContatoCommandTests
{
    private static readonly CriarUsuarioCommand Command = new("test@test.com", "Gabriel Test", "987654321");
    
    private readonly CriarUsuarioCommandHandler _handler;
    private readonly IContatoRepository _contatoRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CriarContatoCommandTests()
    {
        _contatoRepositoryMock = Substitute.For<IContatoRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new(_contatoRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Deve_RetornarSucesso_QuandoTudoEhValido()
    {
        // Arrange
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
        CriarUsuarioCommand emailInvalidoCommand = Command with
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
        CriarUsuarioCommand nomeInvalidoCommand = Command with
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
        CriarUsuarioCommand telefoneInvalido = Command with
        {
            Telefone = "9765490A1"
        };

        // Act
        var result = await _handler.Handle(telefoneInvalido, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TelefoneErrors.FormatoInvalido);
    }
}
