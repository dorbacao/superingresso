using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Web.Api.Database;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddScoped<SuperIngressoContext>(service =>
{
    return new SuperIngressoContextFactory().CreateDbContext(null);
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
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
});

//builder.Services.AddCors();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          //.WithHeaders(HeaderNames.ContentType, "application/json")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
        //.WithMethods("PUT", "DELETE", "GET", "OPTIONS", "POST")
        );
});


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseSwagger();

// Configurar o middleware HTTP
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
        c.RoutePrefix = "swagger"; // Configurar Swagger UI na raiz do aplicativo
    });
}
else
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Minha API v1");
        c.RoutePrefix = "swagger"; // Configurar Swagger UI na raiz do aplicativo
    });
}

app.UseCors("CorsPolicy");
//app.UseCors(builder =>
//    builder.WithOrigins("http://localhost:5173") // Substitua com a origem que você quer permitir
//           .AllowAnyHeader()
//           .AllowAnyMethod()
//);
//app.UseCors(
//               options => options.SetIsOriginAllowed(x => _ = true)
//               .WithMethods("PUT", "DELETE", "GET", "OPTIONS", "POST")
//               .AllowAnyHeader()
//               .AllowCredentials()
//           );

// Configure the HTTP request pipeline.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.UseMiddleware<OptionsMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
