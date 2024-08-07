using Fiap.TechChallenge.Kernel.Contatos;
using FluentAssertions;

namespace Domain.TestsUnits.Contatos;

public class TelefoneTests
{
    [Theory]
    [InlineData("978219087")]
    [InlineData("978219017")]
    public void Criar_Deve_RetornarTelelfone_QuandoValorEhValido(string valor)
    {
        // Arrange
        // Act
        var telefone = Telefone.Criar(valor);

        // Assert
        telefone.IsSuccess.Should().BeTrue();
        telefone.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Criar_Deve_RetornarFalha_QuandoValorEhVazio(string valor)
    {
        // Arrange
        // Act
        var telefone = Telefone.Criar(valor);

        // Assert
        telefone.IsFailure.Should().BeTrue();
        telefone.Error.Should().Be(TelefoneErrors.Vazio);
    }

    [Theory]
    [InlineData("9")]
    [InlineData("95")]
    [InlineData("954")]
    [InlineData("9542")]
    [InlineData("95420")]
    [InlineData("954201")]
    [InlineData("9542014")]
    [InlineData("95420147")]
    [InlineData("9542014788")]
    public void Criar_Deve_RetornarFalha_QuandoTamanhoEhInvalido(string valor)
    {
        // Arrange
        // Act
        var telefone = Telefone.Criar(valor);

        // Assert
        telefone.IsFailure.Should().BeTrue();
        telefone.Error.Should().Be(TelefoneErrors.TamanhoInvalido);
    }

    [Theory]
    [InlineData("9765R4312")]
    [InlineData("9765#4312")]
    [InlineData("9765-4312")]
    [InlineData("9765+4312")]
    public void Criar_Deve_RetornarFalha_QuandoFormatoEhInvalido(string valor)
    {
        // Arrange
        // Act
        var telefone = Telefone.Criar(valor);

        // Assert
        telefone.IsFailure.Should().BeTrue();
        telefone.Error.Should().Be(TelefoneErrors.FormatoInvalido);
    }
}
