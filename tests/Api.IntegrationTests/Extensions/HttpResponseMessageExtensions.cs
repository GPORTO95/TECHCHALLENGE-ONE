using Api.IntegrationTests.Contracts;
using Fiap.TechChallenge.One.Domain.Kernel;
using Newtonsoft.Json;
using System.Text.Json;

namespace Api.IntegrationTests.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<CustomProblemDetails> GetProblemDetails(
        this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Successful response");
        }

        string result = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            IncludeFields = true
        };

        CustomProblemDetails problemDetails = JsonConvert.DeserializeObject<CustomProblemDetails>(result);

        Ensure.NotNull(problemDetails);

        return problemDetails;
    }
}
