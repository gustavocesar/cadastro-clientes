using ErrorOr;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationResultExtensions
{
    public static List<Error> ToErrors(this ValidationResult validationResult) =>
        validationResult.Errors
            .Select(e => Error.Validation(description: e.ErrorMessage))
            .ToList();
}
