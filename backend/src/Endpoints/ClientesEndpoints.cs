using Application.Queries;
using MediatR;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Endpoints;

[ExcludeFromCodeCoverage]
public static class ClientesEndpoints
{
    public static WebApplication MapEndpointsClientes(this WebApplication app)
    {
        var root = "clientes";

        app.MapGet($"/{root}", async (IMediator mediator) =>
        {
            var clientes = await mediator.Send(new ObterTodosClientesQuery());

            return Results.Ok(clientes);
        });

        return app;
    }
}