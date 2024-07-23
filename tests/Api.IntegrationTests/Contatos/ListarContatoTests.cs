using Api.IntegrationTests.Abstractions;
using Api.IntegrationTests.Contracts;
using Api.IntegrationTests.Extensions;
using Fiap.TechChallenge.One.Application.Contatos;
using Fiap.TechChallenge.One.Domain.Ddds;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Api.IntegrationTests.Contatos;

public class ListarContatoTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTests(factory)
{
    [Fact(DisplayName = "Obter lista filtrada")]
    public async Task Deve_RetornarOk_QuandoListaFiltrada()
    {
        // Arrange
        await ContatoFixture.CriarContato(HttpClient);
        await ContatoFixture.CriarContato(HttpClient, "21");
        await ContatoFixture.CriarContato(HttpClient);

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/v1/contatos?ddd=11");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var contatos = await response.Content.ReadFromJsonAsync<List<ContatoResponse>>();

        contatos!.Count.Should().Be(2);
    }

    [Fact(DisplayName = "Obter lista não filtrada")]
    public async Task Deve_RetornarOk_QuandoListaNaoFiltrada()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/v1/contatos");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "Ddd invalido")]
    public async Task Deve_RetornarBadRequest_QuandoDddEhInvalido()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/v1/contatos?ddd=1A");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.ValorInvalido.Description);
    }

    [Fact(DisplayName = "Ddd invalido")]
    public async Task Deve_RetornarBadRequest_QuandoTamanhoDddEhInvalido()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/v1/contatos?ddd=1");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.TamanhoInvalido.Description);
    }
}
