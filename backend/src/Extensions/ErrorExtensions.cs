using ErrorOr;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Extensions;

[ExcludeFromCodeCoverage]
public static class ErrorExtensions
{
    public static IResult ToErrorResult<T>(this ErrorOr<T> result)
    {
        if (result.FirstError == Error.NotFound())
            return Results.NotFound();

        return Results.BadRequest(result.Errors);
    }
}