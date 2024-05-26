using Api.IntegrationTests.Abstractions;
using Fiap.TechChallenge.One.Application.Contatos.Criar;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Api.IntegrationTests.Contatos;

public class CriarContatoTests : BaseFunctionalTests
{
    public CriarContatoTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoEmailEhVazio()
    {
        // Arrange
        CriarContatoCommand request = new("", "Nome Completo", "956789087", "11");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
