using CadastroCliente.Application.Commands.Interfaces;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Queries.Interfaces;
using CadastroCliente.Extensions;
using System.Diagnostics.CodeAnalysis;


namespace CadastroCliente.Endpoints;

[ExcludeFromCodeCoverage]
public static class PessoaFisicaEndpoints
{
    public static WebApplication MapEndpointsPessoasFisicas(this WebApplication app)
    {
        var root = "pessoas-fisicas";

        app.MapGet($"/{root}/{{id}}", async (Guid id, IPessoaFisicaQuery query) =>
        {
            var response = await query.ObterPorId(id);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapPost($"/{root}", async (PessoaFisicaCommandRequest request, IPessoaFisicaCommand command) =>
        {
            var response = await command.Criar(request);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Created($"/{root}/{response.Value.Id}", response.Value);
        });

        app.MapPut($"/{root}/{{id}}", async (Guid id, PessoaFisicaCommandRequest request, IPessoaFisicaCommand command) =>
        {
            var response = await command.Atualizar(id, request);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapDelete($"/{root}/{{id}}", async (Guid id, IPessoaFisicaCommand command) =>
        {
            var response = await command.Deletar(id);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        return app;
    }
}