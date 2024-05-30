using Api.IntegrationTests.Abstractions;
using Api.IntegrationTests.Contracts;
using Api.IntegrationTests.Extensions;
using Fiap.TechChallenge.One.Application.Contatos.Atualizar;
using Fiap.TechChallenge.One.Application.Contatos.Criar;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Ddds;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Api.IntegrationTests.Contatos;

public class AtualizarContatoTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTests(factory)
{
    private readonly AtualizarContatoCommand Command = new(Guid.NewGuid(), "email@teste.com", "Nome Completo", "954123214", "11");

    [Fact]
    public async Task Deve_RetornarNotFound_QuandoContatoNaoExiste()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", Command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Title.Should().Be("Contatos.NaoEncontrado");
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoEmailEhVazio()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Email = "", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(EmailErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoEmailEhInvalido()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Email = "invalido", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(EmailErrors.FormatoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoNomeEhVazio()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Nome = "", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoNomeEhIncompleto()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Nome = "Teste", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.NomeIncompleto.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoNomeEhInvalido()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Nome = "Teste 123", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(NomeErrors.FormatoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTelefoneEhVazio()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Telefone = "", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(TelefoneErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTelefoneEhInvalido()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Telefone = "9875423A2", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(TelefoneErrors.FormatoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTamanhoTelefoneEhInvalido()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Telefone = "98754232", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(TelefoneErrors.TamanhoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoDddEhVazio()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Ddd = "", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.Vazio.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoTamanhoDddEhInvalido()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Ddd = "1", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.TamanhoInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoDddEhInvalido()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Ddd = "1A", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.ValorInvalido.Description);
    }

    [Fact]
    public async Task Deve_RetornarBadRequest_QuandoDddNaoExiste()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Ddd = "00", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Title.Should().Be("Ddd.NaoEncontrado");
    }

    [Fact]
    public async Task Deve_RetornarOk_QuandoContatoEhAtualizado()
    {
        // Arrange
        Guid contatoId = await ContatoFixture.CriarContato(HttpClient);
        AtualizarContatoCommand request = Command with { Ddd = "21", ContatoId = contatoId };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/v1/contatos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
