namespace Fiap.TechChallenge.Exclusao.IntegrationTests.Abstractions;

public abstract class BaseFunctionalTests : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient { get; init; }

    public BaseFunctionalTests(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
}
