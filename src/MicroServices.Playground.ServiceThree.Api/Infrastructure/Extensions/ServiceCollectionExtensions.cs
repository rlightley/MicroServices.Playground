using MicroServices.Playground.ServiceThree.Api.Infrastructure.ApiClients;
using Polly;
using Refit;

namespace MicroServices.Playground.ServiceThree.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterRefitClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddRefitClient<IServiceOneApiClient>()
            .ConfigureHttpClient(c => { c.BaseAddress = new Uri(config["ServiceUrls:ServiceOne"]); })
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
            }));

        services.AddRefitClient<IServiceTwoApiClient>()
            .ConfigureHttpClient(c => { c.BaseAddress = new Uri(config["ServiceUrls:ServiceTwo"]); })
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
            }));
    }
}
