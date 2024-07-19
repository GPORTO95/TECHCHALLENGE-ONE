using Prometheus;

namespace Fiap.TechChallenge.One.API.Endpoints
{
    public class MetricsEndPoint : IEndpoint
    {

        private static readonly Counter metricsRequests = Metrics.CreateCounter("metrics_requests_total", "Total number of requests to the metrics endpoint");

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/metrics", () =>
            {
                metricsRequests.Inc();
                return Results.Ok();
            });
        }
    }
}
