using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Queries;
using CadastroCliente.Extensions;
using MediatR;
using System.Diagnostics.CodeAnalysis;


namespace CadastroCliente.Endpoints;

[ExcludeFromCodeCoverage]
public static class PessoaFisicaEndpoints
{
    public static WebApplication MapEndpointsPessoasFisicas(this WebApplication app)
    {
        var root = "pessoas-fisicas";

        app.MapGet($"/{root}/{{id}}", async (Guid id, IMediator mediator) =>
        {
            var query = new ObterPessoaFisicaQuery { Id = id };
            var response = await mediator.Send(query);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapPost($"/{root}", async (CadastrarPessoaFisicaCommand command, IMediator mediator) =>
        {
            var response = await mediator.Send(command);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Created($"/{root}/{response.Value.Id}", response.Value);
        });

        app.MapPut($"/{root}/{{id}}", async (Guid id, AlterarPessoaFisicaCommand command, IMediator mediator) =>
        {
            command.Id = id;
            var response = await mediator.Send(command);

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        app.MapDelete($"/{root}/{{id}}", async (Guid id, IMediator mediator) =>
        {
            var response = await mediator.Send(new DeletarPessoaFisicaCommand { Id = id });

            if (response.IsError)
                return response.ToErrorResult();

            return Results.Ok(response.Value);
        });

        return app;
    }
}