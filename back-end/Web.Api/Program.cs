using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .ConfigureApi()
    .ConfigureCors()
    .ConfigureSwagger()
    .ConfigureAuthentication(builder.Configuration)
    .ConfigureServices()
    ;

builder.ConfigureSettings();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.UseMiddleware<OptionsMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseForwardedHeaders();

app.MapControllers();

app.Run();
