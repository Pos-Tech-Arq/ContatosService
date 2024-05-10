using ContatosService.Api.Requests;
using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Infra.Configurations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.AddDomainService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/v1/contatos", async (ICriaContatoService criaContatoService, [FromBody] CriaContatoRequest request) =>
    {
        await criaContatoService.Handle(new CriaContatoCommand(request.Telefone.Ddd, request.Telefone.Numero, request.Nome,
            request.Email));

        return Results.Accepted();
    })
    .WithName("CriaContato")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}