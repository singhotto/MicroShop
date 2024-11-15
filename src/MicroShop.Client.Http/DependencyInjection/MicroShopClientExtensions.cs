using MicroShop.Client.Http;
using MicroShop.Client.Http.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class MicroShopClientExtensions {

    public static IServiceCollection AddTransactionRepClient(this IServiceCollection services, IConfiguration configuration) {

        IConfigurationSection confSection = configuration.GetSection(MicroShopClientOptions.SectionName);
        MicroShopClientOptions options = confSection.Get<MicroShopClientOptions>() ?? new();

        services.AddHttpClient<IMicroShopClient, MicroShopClient>(o => {          
            o.BaseAddress = new Uri(options.BaseAddress);
        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        });

        return services;
    }

}
