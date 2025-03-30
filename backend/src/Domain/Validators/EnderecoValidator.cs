using FluentValidation;
using CadastroCliente.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Domain.Validators;

[ExcludeFromCodeCoverage]
public class EnderecoValidator : AbstractValidator<Endereco>
{
    public EnderecoValidator()
    {
        RuleFor(x => x.CEP)
            .Length(8)
            .When(x => !string.IsNullOrEmpty(x.CEP))
            .WithMessage(x => "CEP deve conter 8 dígitos " + x.CEP);

        RuleFor(x => x.Logradouro)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Logradouro))
            .WithMessage("Logradouro é obrigatório e deve ter no máximo 100 caracteres");

        RuleFor(x => x.Numero)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.Numero))
            .WithMessage("Número é obrigatório e deve ter no máximo 10 caracteres");

        RuleFor(x => x.Bairro)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Bairro))
            .WithMessage("Bairro é obrigatório e deve ter no máximo 50 caracteres");

        RuleFor(x => x.Cidade)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Cidade))
            .WithMessage("Cidade é obrigatória e deve ter no máximo 50 caracteres");

        RuleFor(x => x.Estado)
            .Length(2)
            .WithMessage("Estado deve conter 2 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Estado));
    }
}