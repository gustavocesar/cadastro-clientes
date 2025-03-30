using CadastroCliente.Application.Commands.Interfaces;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Queries.Interfaces;
using CadastroCliente.Extensions;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Endpoints;

[ExcludeFromCodeCoverage]
public static class PessoaJuridicaEndpoints
{
    public static WebApplication MapEndpointsPessoasJuridicas(this WebApplication app)
    {
        var root = "pessoas-juridicas";

        app.MapGet($"/{root}/{{id}}", async (Guid id, IPessoaJuridicaQuery query) =>
        {
            var response = await query.ObterPorId(id);

            if (response.IsError)
                return Results.NotFound();

            return Results.Ok(response.Value);
        });

        app.MapPost($"/{root}", async (PessoaJuridicaCommandRequest request, IPessoaJuridicaCommand command) =>
        {
            var response = await command.Criar(request);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapPut($"/{root}/{{id}}", async (Guid id, PessoaJuridicaCommandRequest request, IPessoaJuridicaCommand command) =>
        {
            var response = await command.Atualizar(id, request);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapDelete($"/{root}/{{id}}", async (Guid id, IPessoaJuridicaCommand command) =>
        {
            var response = await command.Deletar(id);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        return app;
    }
}
