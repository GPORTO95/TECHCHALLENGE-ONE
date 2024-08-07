using Bogus;
using Fiap.TechChallenge.Kernel.Ddds;
using Fiap.TechChallenge.Listagem.API.Application.Contatos.Listar;
using Fiap.TechChallenge.Listagem.IntegrationTests.Abstractions;
using FluentAssertions;
using Integration.BaseTests.Contracts;
using Integration.BaseTests.Fixture;
using System.Net;
using System.Net.Http.Json;
using Integration.BaseTests.Extensions;

namespace Api.IntegrationTests.Contatos;

public class ListarContatoTests : BaseFunctionalTests
{
    private readonly ContatoFixture _contatoFixture;

    public ListarContatoTests(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
        _contatoFixture = new(factory._msSqlContainer.GetConnectionString());
    }

    [Fact(DisplayName = "Obter lista filtrada")]
    public async Task Deve_RetornarOk_QuandoListaFiltrada()
    {
        // Arrange
        Guid id1 = Guid.NewGuid();
        Guid id3 = Guid.NewGuid();
        Guid[] ids = [id1, id3];

        var faker = new Faker("pt_BR");

        await _contatoFixture.CriarContato(
            id1,
            faker.Person.FullName,
            faker.Person.Email,
            faker.Phone.PhoneNumber("9########"),
            "11");

        await _contatoFixture.CriarContato(
            Guid.NewGuid(),
            faker.Person.FullName,
            faker.Person.Email,
            faker.Phone.PhoneNumber("9########"),
            "15");

        await _contatoFixture.CriarContato(
            id3,
            faker.Person.FullName,
            faker.Person.Email,
            faker.Phone.PhoneNumber("9########"),
            "11");

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/contatos?ddd=11");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var contatos = await response.Content.ReadFromJsonAsync<List<ContatoResponse>>();

        contatos!
            .Where(c => ids.Contains(c.ContatoId))
            .ToList()
            .Count
            .Should()
            .Be(2);
    }

    [Fact(DisplayName = "Obter lista não filtrada")]
    public async Task Deve_RetornarOk_QuandoListaNaoFiltrada()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/contatos");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "Ddd invalido")]
    public async Task Deve_RetornarBadRequest_QuandoDddEhInvalido()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/contatos?ddd=1A");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.ValorInvalido.Description);
    }

    [Fact(DisplayName = "Ddd tamanho invalido")]
    public async Task Deve_RetornarBadRequest_QuandoTamanhoDddEhInvalido()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/contatos?ddd=1");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Detail.Should().Be(CodigoErrors.TamanhoInvalido.Description);
    }
}
