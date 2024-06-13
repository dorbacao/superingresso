using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

public static class ConfigureApiServiceExtension
{
    public static IServiceCollection ConfigureApi(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            ;

        serviceCollection.AddEndpointsApiExplorer();

        return serviceCollection;
    }
    public static IServiceCollection ConfigureCors(this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }

        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy",
                builder => builder.AllowAnyOrigin()
                                  //.WithHeaders(HeaderNames.ContentType, "application/json")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                //.WithMethods("PUT", "DELETE", "GET", "OPTIONS", "POST")
                );
        });

        return serviceCollection;

    }
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }



        var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

        var secret = Encoding.ASCII.GetBytes(config["Jwt:Secret"]); ;

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = true;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = config["Jwt:ValidIssuer"],
                ValidAudience = config["Jwt:ValidAudience"],
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret)
            };

        });

        return serviceCollection;

    }
}
