using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

public static class ConfigureSwaggerServiceExtension
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }

        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Minha API",
                Version = "v1",
                Description = "Uma API simples para demonstrar o Swagger com ASP.NET Core 7",
                Contact = new OpenApiContact
                {
                    Name = "Seu Nome",
                    Email = "seuemail@example.com"
                }
            });
            c.ConfigureSwaggerAreas();
            c.ConfigureSwaggerSecuritySchema();
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return serviceCollection;

    }
    public static void ConfigureSwaggerSecuritySchema(this SwaggerGenOptions options)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Put only your token in text box. Don't put 'Bearer' initial string",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

        //Middleware responsável por habilitar parametros da rota como parte da assinatura da rota.
        //Por exemplo, imaginem que temos duas duas actions para uma mesma rota
        //options.ResolveConflictingActions(description => description.First());
    }
    public static void ConfigureSwaggerAreas(this SwaggerGenOptions c)
    {
        c.TagActionsBy(api =>
        {
            if (api.GroupName != null)
            {
                return new[] { api.GroupName };
            }

            var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                return new[] { controllerActionDescriptor.ControllerName };
            }

            throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });

        c.DocInclusionPredicate((name, api) => true);
    }
}
