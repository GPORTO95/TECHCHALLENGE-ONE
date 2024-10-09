using Fiap.TechChallenge.Exclusao.IntegrationTests.Abstractions;
using FluentAssertions;
using Integration.BaseTests.Contracts;
using System.Net;
using Integration.BaseTests.Extensions;
using Bogus;
using Integration.BaseTests.Fixture;

namespace Fiap.TechChallenge.Exclusao.IntegrationTests;


public class ExcluirContatoTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTests(factory)
{
    private readonly ContatoFixture _contatoFixture = new(factory._msSqlContainer.GetConnectionString());

    [Fact]
    public async Task Deve_RetornarNotFound_QuandoContatoNaoExiste()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.DeleteAsync($"api/contatos/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Title.Should().Be("Contatos.NaoEncontrado");
    }

    [Fact]
    public async Task Deve_RetornarOk_QuandoContatoExiste()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        var faker = new Faker("pt_BR");

        await _contatoFixture.CriarContato(
            id,
            faker.Person.FullName,
            faker.Person.Email,
            faker.Phone.PhoneNumber("9########"),
            "11");

        // Act
        HttpResponseMessage response = await HttpClient.DeleteAsync($"api/contatos/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
