using Fiap.TechChallenge.One.Domain.Ddds;
using FluentAssertions;

namespace Domain.UnitTests.Ddds;

public class CodigoTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Criar_DeveRetornarErro_QuandoCodigoRegiaoEhVazio(string valor)
    {
        // Arrange
        // Act
        var codigoRegiao = Codigo.Criar(valor);

        // Assert
        codigoRegiao.IsFailure.Should().BeTrue();
        codigoRegiao.Error.Should().Be(CodigoErrors.Vazio);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("1")]
    [InlineData("12890")]
    public void Criar_DeveRetornarErro_QuandoCodigoRegiaoTemTamanhoInvalido(string valor)
    {
        // Arrange
        // Act
        var codigoRegiao = Codigo.Criar(valor);

        // Assert
        codigoRegiao.IsFailure.Should().BeTrue();
        codigoRegiao.Error.Should().Be(CodigoErrors.TamanhoInvalido);
    }

    [Theory]
    [InlineData("1A")]
    [InlineData("AA")]
    [InlineData("A9")]
    public void Criar_DeveRetornarErro_QuandoCodigoRegiaoTemFormatoInvalido(string valor)
    {
        // Arrange
        // Act
        var codigoRegiao = Codigo.Criar(valor);

        // Assert
        codigoRegiao.IsFailure.Should().BeTrue();
        codigoRegiao.Error.Should().Be(CodigoErrors.ValorInvalido);
    }

    [Theory]
    [InlineData("11")]
    [InlineData("21")]
    [InlineData("99")]
    public void Criar_DeveRetornarSucesso_QuandoCodigoRegiaoEhValido(string valor)
    {
        // Arrange
        // Act
        var codigoRegiao = Codigo.Criar(valor);

        // Assert
        codigoRegiao.IsSuccess.Should().BeTrue();
    }
}
