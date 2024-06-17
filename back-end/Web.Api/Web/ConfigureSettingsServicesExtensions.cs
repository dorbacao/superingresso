using Web.Api.Infraestrutura.Authentication;
using Web.Api.Infraestrutura.Authentication.Google;

public static class ConfigureSettingsServicesExtensions
{
    public static WebApplicationBuilder ConfigureSettings(this WebApplicationBuilder builder)
    {

        builder.Services.Configure<GoogleAuthConfig>(builder.Configuration.GetSection("Google"));
        builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));

        return builder;
    }
}
