using Fiap.TechChallenge.One.Domain.Contatos;
using FluentAssertions;

namespace Domain.TestsUnits.Contatos;

public class EmailTests
{
    [Theory]
    [InlineData("teste@gmail.com")]
    [InlineData("teste123@hotmail.com")]
    [InlineData("teste123_7298@outlook.com.br")]
    public void Criar_Deve_RetornarEmail_QuandoValorEhValido(string valor)
    {
        // Arrange
        // Act
        var email = Email.Criar(valor);

        // Assert
        email.IsSuccess.Should().BeTrue();
        email.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Criar_Deve_RetornarFalha_QuandoValorEhVazio(string valor)
    {
        // Arrange
        // Act
        var email = Email.Criar(valor);

        // Assert
        email.IsFailure.Should().BeTrue();
        email.Error.Should().Be(EmailErrors.Vazio);
    }

    [Theory]
    [InlineData("teste123")]
    [InlineData("teste123_7298@outlook")]
    public void Criar_Deve_RetornarFalha_QuandoValorEhInvalido(string valor)
    {
        // Arrange
        // Act
        var email = Email.Criar(valor);

        // Assert
        email.IsFailure.Should().BeTrue();
        email.Error.Should().Be(EmailErrors.FormatoInvalido);
    }
}
