using Fiap.TechChallenge.Kernel;
using Integration.BaseTests.Contracts;
using Newtonsoft.Json;

namespace Integration.BaseTests.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<CustomProblemDetails> GetProblemDetails(
        this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Successful response");
        }

        string result = await response.Content.ReadAsStringAsync();

        CustomProblemDetails? problemDetails = JsonConvert.DeserializeObject<CustomProblemDetails>(result);

        Ensure.NotNull(problemDetails);

        return problemDetails;
    }
}
