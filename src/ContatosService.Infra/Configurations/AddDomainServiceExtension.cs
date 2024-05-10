﻿using ContatosService.Domain.Contracts;
using ContatosService.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContatosService.Infra.Configurations;

public static class AddDomainServiceExtension
{
    public static void AddDomainService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICriaContatoService, CriaContatoService>();
    }
}