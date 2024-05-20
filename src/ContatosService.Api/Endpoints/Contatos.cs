using ContatosService.Api.Requests;
using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ContatosService.Api.Endpoints;

public static class Contatos
{
    public static void RegisterContatosEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/v1/contatos");
        
        users.MapPost("", async (ICriaContatoService criaContatoService, [FromBody] CriaContatoRequest request) =>
            {
                await criaContatoService.Handle(new CriaContatoCommand(request.Telefone.Ddd, request.Telefone.Numero,
                    request.Nome,
                    request.Email));

                return Results.Accepted();
            })
            .WithName("CriaContato")
            .WithOpenApi()
            .AddFluentValidationFilter();

        users.MapGet("", async (IContatosRepository ContatosRepository, [FromQuery] string? Ddd) =>
            {
                var contatos = await ContatosRepository.BuscaRegiao(Ddd);

                return Results.Ok(contatos);
            })
            .WithName("BuscaContatos")
            .WithOpenApi()
            .AddFluentValidationFilter();
    }
}