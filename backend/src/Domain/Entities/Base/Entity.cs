using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Domain.Entities.Base;

[ExcludeFromCodeCoverage]
public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public abstract ValidationResult Validate();
}