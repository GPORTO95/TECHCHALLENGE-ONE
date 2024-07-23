using Api.IntegrationTests.Abstractions;
using Api.IntegrationTests.Contracts;
using Api.IntegrationTests.Extensions;
using FluentAssertions;
using System.Net;

namespace Api.IntegrationTests.Contatos;


public class ExcluirContatoTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTests(factory)
{
    [Fact]
    public async Task Deve_RetornarNotFound_QuandoContatoNaoExiste()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.DeleteAsync($"api/v1/contatos/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Title.Should().Be("Contatos.NaoEncontrado");
    }

    [Fact]
    public async Task Deve_RetornarOk_QuandoContatoExiste()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);

        // Act
        HttpResponseMessage response = await HttpClient.DeleteAsync($"api/v1/contatos/{contatoId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
