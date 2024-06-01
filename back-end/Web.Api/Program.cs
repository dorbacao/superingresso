using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Web.Api.Database;

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Configurar o middleware HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
        c.RoutePrefix = "swagger"; // Configurar Swagger UI na raiz do aplicativo
    });
}

app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
