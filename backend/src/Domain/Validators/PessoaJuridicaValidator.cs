using CadastroCliente.Domain.Entities;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Domain.Validators;

[ExcludeFromCodeCoverage]
public class PessoaJuridicaValidator : AbstractValidator<PessoaJuridica>
{
    public PessoaJuridicaValidator()
    {
        RuleFor(x => x.RazaoSocial)
            .NotEmpty()
            .WithMessage("Razão social é obrigatória")
            .MinimumLength(3)
            .WithMessage("Razão social é obrigatória");

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("CNPJ é obrigatório")
            .Must(value => DocumentoUtils.ValidarCnpj(value))
            .WithMessage("CNPJ inválido");

        RuleFor(x => x.InscricaoEstadual)
            .NotEmpty()
            .When(x => !x.Isento)
            .WithMessage("Inscrição estadual é obrigatória");

        RuleFor(x => x.Isento)
            .NotEmpty()
            .When(x => x.InscricaoEstadual is null)
            .WithMessage("Isento é obrigatório");
    }
}
