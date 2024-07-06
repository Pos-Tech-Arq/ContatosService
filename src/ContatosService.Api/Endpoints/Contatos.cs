using ContatosService.Api.Requests;
using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ContatosService.Api.Endpoints;

public static class Contatos
{
    public static void RegisterContatosEndpoints(this IEndpointRouteBuilder routes)
    {
        var contatoRoute = routes.MapGroup("/api/v1/contatos");

        contatoRoute.MapPost("",
                async (ICriaContatoService criaContatoService, [FromBody] CriaContatoRequest request) =>
                {
                    await criaContatoService.Handle(new CriaContatoCommand(request.Telefone.Ddd,
                        request.Telefone.Numero,
                        request.Nome,
                        request.Email));

                    return TypedResults.NoContent();
                })
            .WithName("CriaContato")
            .WithOpenApi()
            .AddFluentValidationFilter();

        contatoRoute.MapGet("", async (IContatosRepository contatosRepository, [FromQuery] string? ddd) =>
            {
                var contatos = await contatosRepository.BuscaRegiao(ddd);

                return TypedResults.Ok(contatos);
            })
            .WithName("BuscaContatos")
            .WithOpenApi()
            .AddFluentValidationFilter();

        contatoRoute.MapPut("{id}",
                async (IAtualizaContatoService atualizaContatoService, [FromBody] AtualizaContatoRequest request,[FromRoute] Guid id) =>
                {
                    await atualizaContatoService.Handle(new AtualizaContatoCommand(id, request.Telefone.Ddd,
                        request.Telefone.Numero,
                        request.Nome,
                        request.Email));

                    return TypedResults.NoContent();
                })
            .WithName("AtualizaContato")
            .WithOpenApi()
            .AddFluentValidationFilter();

        contatoRoute.MapDelete("{id}", async (IRemoveContatoService removeContatoService, [FromRoute] Guid id) =>
            {
                await removeContatoService.Handle(new RemoveContatoCommand(id));

                return TypedResults.NoContent();
            })
            .WithName("RemoveContato")
            .WithOpenApi()
            .AddFluentValidationFilter();
    }
}