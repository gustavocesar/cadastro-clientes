using CadastroCliente.Application.Queries.Interfaces;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Endpoints;

[ExcludeFromCodeCoverage]
public static class ClientesEndpoints
{
    public static WebApplication MapEndpointsClientes(this WebApplication app)
    {
        var root = "clientes";

        app.MapGet($"/{root}", async (IClientesQuery query) =>
        {
            var clientes = await query.ObterTodos();

            return Results.Ok(clientes);
        });

        return app;
    }
}