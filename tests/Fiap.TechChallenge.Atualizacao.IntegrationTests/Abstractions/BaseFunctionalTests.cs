namespace Fiap.TechChallenge.Atualizacao.IntegrationTests;

public abstract class BaseFunctionalTests : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient { get; init; }

    public BaseFunctionalTests(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
}
