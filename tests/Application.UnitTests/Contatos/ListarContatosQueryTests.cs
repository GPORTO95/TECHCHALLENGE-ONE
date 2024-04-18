using Fiap.TechChallenge.One.Application.Contatos;
using Fiap.TechChallenge.One.Application.Contatos.Listar;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Ddds;
using Fiap.TechChallenge.One.Domain.Kernel;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Contatos;

public class ListarContatosQueryTests
{
    private static readonly ListarContatosQuery Query = new(null);

    private readonly ListarContatosQueryHandler _handler;
    private readonly IContatoRepository _contatoRepository;

    public ListarContatosQueryTests()
    {
        _contatoRepository = Substitute.For<IContatoRepository>();

        _handler = new(_contatoRepository);
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoDddNaoEhValido()
    {
        // Arrange
        ListarContatosQuery queryInvalid = Query with
        {
            Ddd = "1A"
        };

        // Act
        Result<IEnumerable<ContatoResponse>> result = await _handler.Handle(queryInvalid, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CodigoErrors.ValorInvalido);
    }

    [Fact]
    public async Task Handle_DeveRetornarSucesso_QuandoListaVazia()
    {
        // Arrange
        _contatoRepository.ListarAsync(Arg.Any<Codigo>(), Arg.Any<CancellationToken>())
            .Returns([]);

        // Act
        Result<IEnumerable<ContatoResponse>> result = await _handler.Handle(Query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_DeveRetornarSucesso_QuandoListaNaoEhVazia()
    {
        // Arrange
        Contato contato1 = Contato.Criar(
            Nome.Criar("Gabriel Teste").Value,
            Email.Criar("gabriel.test@test.com").Value,
            Telefone.Criar("987654321").Value,
            Guid.NewGuid()).Value;

        Contato contato2 = Contato.Criar(
            Nome.Criar("Gabriel Teste Test").Value,
            Email.Criar("gabriel.test.3@test.com").Value,
            Telefone.Criar("987654323").Value,
            Guid.NewGuid()).Value;

        List<Contato> contatos = [contato1, contato2];

        _contatoRepository.ListarAsync(Arg.Any<Codigo>(), Arg.Any<CancellationToken>())
            .Returns(contatos);

        // Act
        Result<IEnumerable<ContatoResponse>> result = await _handler.Handle(Query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}
