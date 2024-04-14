using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Application.Contatos.Excluir;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Kernel;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Contatos;

public class ExcluirContatoCommandTests
{
    private readonly static ExcluirContatoCommand Command = new(Guid.NewGuid());

    private readonly ExcluirContatoCommandHandler _handler;
    private readonly IContatoRepository _contatoRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public ExcluirContatoCommandTests()
    {
        _contatoRepositoryMock = Substitute.For<IContatoRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new(_contatoRepositoryMock, _unitOfWorkMock);
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
        _contatoRepositoryMock.Received(1).Excluir(Arg.Is<Contato>(u => u.Id == contato.Id));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
