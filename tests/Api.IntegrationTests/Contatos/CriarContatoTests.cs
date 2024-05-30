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
    private readonly CriarContatoCommand Command = new("email@teste.com", "Nome Completo", "954123214", "11");

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoEmailEhVazio()
    {
        // Arrange
        CriarContatoCommand request = Command with { Email = "" };

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
        CriarContatoCommand request = Command with { Email = "invalido" };

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
        CriarContatoCommand request = Command with { Nome = "" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTelefoneEhVazio()
    {
        // Arrange
        CriarContatoCommand request = Command with { Telefone = "" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(TelefoneErrors.Vazio.Description);
    }
}
