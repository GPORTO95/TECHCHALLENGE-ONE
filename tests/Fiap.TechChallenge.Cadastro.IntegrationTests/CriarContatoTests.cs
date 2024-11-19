using Fiap.TechChallenge.Cadastro.API.Commands;
using Fiap.TechChallenge.Cadastro.IntegrationTests.Abstractions;
using Fiap.TechChallenge.Kernel.Contatos;
using FluentAssertions;
using Integration.BaseTests.Contracts;
using System.Net;
using System.Net.Http.Json;
using Integration.BaseTests.Extensions;
using Fiap.TechChallenge.Kernel.Ddds;

namespace Fiap.TechChallenge.Cadastro.IntegrationTests;

public class CriarContatoTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTests(factory)
{
    private readonly CriarContatoCommand Command = new("email@teste.com", "Nome Completo", "954123214", "11");

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoEmailEhVazio()
    {
        // Arrange
        CriarContatoCommand request = Command with { Email = "" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

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
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

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
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoNomeEhIncompleto()
    {
        // Arrange
        CriarContatoCommand request = Command with { Nome = "Teste" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.NomeIncompleto.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoNomeEhInvalido()
    {
        // Arrange
        CriarContatoCommand request = Command with { Nome = "Teste 123" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.FormatoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTelefoneEhVazio()
    {
        // Arrange
        CriarContatoCommand request = Command with { Telefone = "" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(TelefoneErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTelefoneEhInvalido()
    {
        // Arrange
        CriarContatoCommand request = Command with { Telefone = "9875423A2" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(TelefoneErrors.FormatoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTamanhoTelefoneEhInvalido()
    {
        // Arrange
        CriarContatoCommand request = Command with { Telefone = "98754232" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(TelefoneErrors.TamanhoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoDddEhVazio()
    {
        // Arrange
        CriarContatoCommand request = Command with { Ddd = "" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTamanhoDddEhInvalido()
    {
        // Arrange
        CriarContatoCommand request = Command with { Ddd = "1" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.TamanhoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoDddEhInvalido()
    {
        // Arrange
        CriarContatoCommand request = Command with { Ddd = "1A" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.ValorInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoDddNaoExiste()
    {
        // Arrange
        CriarContatoCommand request = Command with { Ddd = "00" };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Title.Should().Be("Ddd.NaoEncontrado");
    }

    [Fact]
    public async Task Deve_RetornarOk_QuandoContatoEhValido()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", Command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        Guid contatoId = await response.Content.ReadFromJsonAsync<Guid>();

        contatoId.Should().NotBeEmpty();
    }
}

public static class ContatoFixture
{
    public static async Task<Guid> CriarContato(HttpClient HttpClient, string ddd = "11")
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contatos", new CriarContatoCommand("email@teste.com", "Nome Completo", "954123214", ddd));

        Guid contatoId = await response.Content.ReadFromJsonAsync<Guid>();

        return contatoId;
    }
}