using ContatosService.Api.Endpoints;
using ContatosService.Api.Requests.Validators;
using ContatosService.Infra.Configurations;
using ContatosService.Infra.Contexts;
using ContatosService.Infra.ExternalServices.BrasilApiService;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.AddDomainService();
builder.Services.AddBrasilApiClientExtensions(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CriarContatoRequestValidator>();
builder.AddFluentValidationEndpointFilter();
builder.Services.AddHealthChecks().ForwardToPrometheus();

builder.Services.UseHttpClientMetrics();
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation();
        metrics.AddRuntimeInstrumentation();
        metrics.AddPrometheusExporter();

        metrics.AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel");
        metrics.AddView("request-duration",
            new ExplicitBucketHistogramConfiguration
            {
                Boundaries = new[] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.7, 1, 2.5, 5, 10 },
            });
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpMetrics();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.RegisterContatosEndpoints();
app.MapMetrics();

app.Run();

public partial class Program
{
}