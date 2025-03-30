using FluentValidation;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Enums;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Domain.Validators;

[ExcludeFromCodeCoverage]
public class ClienteValidator : AbstractValidator<Cliente>
{
    public ClienteValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email inválido");

        RuleFor(x => x.Telefone)
            .NotEmpty()
            .Matches(@"^\d{10,11}$")
            .WithMessage("Telefone deve conter 10 ou 11 dígitos");

        RuleFor(x => x.Tipo)
            .NotEmpty()
            .Must(tipo => tipo == nameof(TipoCliente.PessoaFisica) || tipo == nameof(TipoCliente.PessoaJuridica))
            .WithMessage("Tipo deve ser PF ou PJ");

        RuleFor(x => x.Endereco)
            .SetValidator(new EnderecoValidator());
    }
}