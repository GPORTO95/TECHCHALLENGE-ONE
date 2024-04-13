using Fiap.TechChallenge.One.Application.Contatos;
using Fiap.TechChallenge.One.Application.Contatos.ObterPorId;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Kernel;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Contatos;

public class ObterContatoPorIdQueryTests
{
    private static readonly Contato Contato = Contato.Criar(
        Nome.Criar("Gabriel Teste").Value,
        Email.Criar("gabriel.test@test.com").Value,
        Telefone.Criar("987654321").Value).Value;

    private static readonly ObterContatoPorIdQuery Query = new(
       Contato.Id);

    private readonly ObterContatoPorIdQueryHandler _handler;
    private readonly IContatoRepository _contatoRepositoryMock;

    public ObterContatoPorIdQueryTests()
    {
        _contatoRepositoryMock = Substitute.For<IContatoRepository>();

        _handler = new(_contatoRepositoryMock);
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoContatoNaoExiste()
    {
        // Arrange
        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(c => c == Query.ContatoId), Arg.Any<CancellationToken>())
            .Returns((Contato?)null);

        // Act
        Result<ContatoResponse> result = await _handler.Handle(Query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ContatoErrors.NaoEncontrado(Query.ContatoId));
    }

    [Fact]
    public async Task Handle_DeveRetornarSucesso_QuandoContatoExiste()
    {
        // Arrange
        _contatoRepositoryMock.ObterPorIdAsync(
            Arg.Is<Guid>(c => c == Query.ContatoId), Arg.Any<CancellationToken>())
            .Returns(Contato);

        // Act
        Result<ContatoResponse> result = await _handler.Handle(Query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(Contato.Id);
    }
}
