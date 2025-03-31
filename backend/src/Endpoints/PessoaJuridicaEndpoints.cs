using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Queries;
using CadastroCliente.Extensions;
using MediatR;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Endpoints;

[ExcludeFromCodeCoverage]
public static class PessoaJuridicaEndpoints
{
    public static WebApplication MapEndpointsPessoasJuridicas(this WebApplication app)
    {
        var root = "pessoas-juridicas";

        app.MapGet($"/{root}/{{id}}", async (Guid id, IMediator mediator) =>
        {
            var query = new ObterPessoaJuridicaQuery { Id = id };
            var response = await mediator.Send(query);

            if (response.IsError)
                return Results.NotFound();

            return Results.Ok(response.Value);
        });

        app.MapPost($"/{root}", async (CadastrarPessoaJuridicaCommand command, IMediator mediator) =>
        {
            var response = await mediator.Send(command);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapPut($"/{root}/{{id}}", async (Guid id, AlterarPessoaJuridicaCommand command, IMediator mediator) =>
        {
            command.Id = id;
            var response = await mediator.Send(command);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapDelete($"/{root}/{{id}}", async (Guid id, IMediator mediator) =>
        {
            var response = await mediator.Send(new DeletarPessoaJuridicaCommand { Id = id });

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        return app;
    }
}
