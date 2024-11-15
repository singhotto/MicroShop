
using Microsoft.Extensions.Configuration;
using Supply.Client.Http;
using Supply.Client.Http.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

public static class SupplyClientExtensions {

    public static IServiceCollection AddTransactionRepClient(this IServiceCollection services, IConfiguration configuration) {

        IConfigurationSection confSection = configuration.GetSection(SupplyClientOptions.SectionName);
        SupplyClientOptions options = confSection.Get<SupplyClientOptions>() ?? new();

        services.AddHttpClient<ISupplyClient, SupplyClient>(o => {          
            o.BaseAddress = new Uri(options.BaseAddress);
        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        });

        return services;
    }

}
