using Fiap.TechChallenge.Kernel.Ddds;
using FluentAssertions;

namespace Domain.UnitTests.Ddds;

public class EstadoTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Criar_DeveRetornarErro_QuandoSiglaEhVazio(string valor)
    {
        // Arrange
        // Act
        var estado = Estado.Criar(valor, "São Paulo");

        // Assert
        estado.IsFailure.Should().BeTrue();
        estado.Error.Should().Be(EstadoErrors.Vazio("Sigla"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Criar_DeveRetornarErro_QuandoDescricaoEhVazio(string valor)
    {
        // Arrange
        // Act
        var estado = Estado.Criar("SP", valor);

        // Assert
        estado.IsFailure.Should().BeTrue();
        estado.Error.Should().Be(EstadoErrors.Vazio("Descrição"));
    }

    [Theory]
    [InlineData("S")]
    [InlineData("SSP")]
    [InlineData("SPPP")]
    public void Criar_DeveRetornarErro_QuandoSiglaEhInvalido(string valor)
    {
        // Arrange
        // Act
        var estado = Estado.Criar(valor, "São Paulo");

        // Assert
        estado.IsFailure.Should().BeTrue();
        estado.Error.Should().Be(EstadoErrors.TamanhoInvalido);
    }

    [Fact]
    public void Criar_DeveRetornarSucesso_QuandoEhValido()
    {
        // Arrange
        // Act
        var estado = Estado.Criar("SP", "São Paulo");

        // Assert
        estado.IsSuccess.Should().BeTrue();
    }
}
