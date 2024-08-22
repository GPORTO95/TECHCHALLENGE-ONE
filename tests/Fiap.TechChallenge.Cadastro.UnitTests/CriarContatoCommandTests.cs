using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Cadastro.API.Commands;
using Fiap.TechChallenge.Cadastro.API.Events;
using Fiap.TechChallenge.Kernel.Contatos;
using Fiap.TechChallenge.Kernel.Ddds;
using FluentAssertions;
using NSubstitute;

namespace Fiap.TechChallenge.Cadastro.UnitTests;

public class CriarContatoCommandTests
{
    private static readonly CriarContatoCommand Command = new("test@test.com", "Gabriel Test", "987654321", "11");
    
    private readonly CriarContatoCommandHandler _handler;
    private readonly IDddRepository _dddRepositoryMock;
    private readonly IEventBus _busMock;

    public CriarContatoCommandTests()
    {
        _dddRepositoryMock = Substitute.For<IDddRepository>();
        _busMock = Substitute.For<IEventBus>();

        _handler = new(_dddRepositoryMock, _busMock);
    }

    [Fact]
    public async Task Handle_Deve_RetornarSucesso_QuandoTudoEhValido()
    {
        // Arrange
        _dddRepositoryMock.ObterPorCodigoAsync(Arg.Is<Codigo>(e => e == Codigo.Criar(Command.Ddd).Value), Arg.Any<CancellationToken>())
            .Returns(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();



        await _busMock.Received(1).PublishAsync(
            Arg.Any<ContatoInseridoEvent>(), 
            Arg.Any<CancellationToken>());
    }

    //[Fact]
    //public async Task Handle_Deve_RetornarErro_QuandoEmailEhInvalido()
    //{
    //    // Arrange
    //    CriarContatoCommand emailInvalidoCommand = Command with
    //    {
    //        Email = "teste@test"
    //    };

    //    // Act
    //    var result = await _handler.Handle(emailInvalidoCommand, default);

    //    // Assert
    //    result.IsFailure.Should().BeTrue();
    //    result.Error.Should().Be(EmailErrors.FormatoInvalido);
    //}

    //[Fact]
    //public async Task Handle_Deve_RetornarErro_QuandoNomeEhInvalido()
    //{
    //    // Arrange
    //    CriarContatoCommand nomeInvalidoCommand = Command with
    //    {
    //        Nome = "Gabriel T3ste"
    //    };

    //    // Act
    //    var result = await _handler.Handle(nomeInvalidoCommand, default);

    //    // Assert
    //    result.IsFailure.Should().BeTrue();
    //    result.Error.Should().Be(NomeErrors.FormatoInvalido);
    //}

    //[Fact]
    //public async Task Handle_Deve_RetornarErro_QuandoTelefoneEhInvalido()
    //{
    //    // Arrange
    //    CriarContatoCommand telefoneInvalido = Command with
    //    {
    //        Telefone = "9765490A1"
    //    };

    //    // Act
    //    var result = await _handler.Handle(telefoneInvalido, default);

    //    // Assert
    //    result.IsFailure.Should().BeTrue();
    //    result.Error.Should().Be(TelefoneErrors.FormatoInvalido);
    //}

    //[Fact]
    //public async Task Handle_Deve_RetornarErro_QuandoDddNaoEhValido()
    //{
    //    // Arrange
    //    CriarContatoCommand dddInvalido = Command with
    //    {
    //        Ddd = "1a"
    //    };

    //    // Act
    //    var result = await _handler.Handle(dddInvalido, default);

    //    // Assert
    //    result.IsFailure.Should().BeTrue();
    //    result.Error.Should().Be(CodigoErrors.ValorInvalido);
    //}

    //[Fact]
    //public async Task Handle_Deve_RetornarErro_QuandoDddNaoExiste()
    //{
    //    // Arrange
    //    _dddRepositoryMock.ObterPorCodigoAsync(Arg.Is<Codigo>(e => e == Codigo.Criar(Command.Ddd).Value), Arg.Any<CancellationToken>())
    //        .Returns(Guid.Empty);

    //    // Act
    //    var result = await _handler.Handle(Command, default);

    //    // Assert
    //    result.IsFailure.Should().BeTrue();
    //    result.Error.Should().Be(DddErrors.CodigoNaoEncontrado(Command.Ddd));
    //}
}
