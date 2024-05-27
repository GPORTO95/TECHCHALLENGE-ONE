using Api.IntegrationTests.Abstractions;
using Api.IntegrationTests.Contracts;
using Api.IntegrationTests.Extensions;
using Fiap.TechChallenge.One.Application.Contatos.Criar;
using Fiap.TechChallenge.One.Domain.Contatos;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Api.IntegrationTests.Contatos;

public class CriarContatoTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTests(factory)
{
    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoEmailEhVazio()
    {
        // Arrange
        CriarContatoCommand request = new("", "Nome Completo", "956789087", "11");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(EmailErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoEmailEhInvalido()
    {
        // Arrange
        CriarContatoCommand request = new("emailinvalido", "Nome Completo", "956789087", "11");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(EmailErrors.FormatoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoNomeEhVazio()
    {
        // Arrange
        CriarContatoCommand request = new("email@teste.com", "", "956789087", "11");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.Vazio.Description);
    }
}
