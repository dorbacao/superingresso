using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Web.Api.Database;
using Web.Api.Infraestrutura;
using static IdentityModel.ClaimComparer;

public static class ConfigureServiceExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }


        serviceCollection.AddScoped<SuperIngressoContext>(service =>
        {
            return new SuperIngressoContextFactory().CreateDbContext(null);
        });

        serviceCollection.AddScoped<GoogleInfraService>();
        serviceCollection.AddScoped<TokenService>();

        return serviceCollection;

    }

}
