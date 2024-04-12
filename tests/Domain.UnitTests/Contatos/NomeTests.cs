using Fiap.TechChallenge.One.Domain.Contatos;
using FluentAssertions;

namespace Domain.TestsUnits.Contatos;


public class NomeTests
{
    [Theory]
    [InlineData("Gabriel Teste")]
    [InlineData("Nome Full Completo")]
    [InlineData("Nome Teste")]
    public void Criar_Deve_RetornarNome_QuandoValorEhValido(string valor)
    {
        // Arrange
        // Act
        var nome = Nome.Criar(valor);

        // Assert
        nome.IsSuccess.Should().BeTrue();
        nome.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Criar_Deve_RetornarFalha_QuandoValorEhVazio(string valor)
    {
        // Arrange
        // Act
        var nome = Nome.Criar(valor);

        // Assert
        nome.IsFailure.Should().BeTrue();
        nome.Error.Should().Be(NomeErrors.Vazio);
    }

    [Theory]
    [InlineData("Gabriel")]
    [InlineData("Teste ")]
    [InlineData("Teste   ")]
    [InlineData(" Teste ")]
    [InlineData("  Teste ")]
    public void Criar_Deve_RetornarFalha_QuandoNomeEhIncompleto(string valor)
    {
        // Arrange
        // Act
        var nome = Nome.Criar(valor);

        // Assert
        nome.IsFailure.Should().BeTrue();
        nome.Error.Should().Be(NomeErrors.NomeIncompleto);
    }

    [Theory]
    [InlineData("Gabriel Tes3ste")]
    [InlineData("Teste @ TESTE")]
    [InlineData("Test# Vasconcelos")]
    [InlineData("Teste Com_Teste")]
    public void Criar_Deve_RetornarFalha_QuandoNomeEhInvalido(string valor)
    {
        // Arrange
        // Act
        var nome = Nome.Criar(valor);

        // Assert
        nome.IsFailure.Should().BeTrue();
        nome.Error.Should().Be(NomeErrors.FormatoInvalido);
    }
}
