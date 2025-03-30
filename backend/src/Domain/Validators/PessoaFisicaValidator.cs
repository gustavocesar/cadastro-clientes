using CadastroCliente.Domain.Entities;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Domain.Validators;

[ExcludeFromCodeCoverage]
public class PessoaFisicaValidator : AbstractValidator<PessoaFisica>
{
    public PessoaFisicaValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MinimumLength(3)
            .WithMessage("Nome é obrigatório");

        RuleFor(x => x.Cpf)
            .NotEmpty()
            .WithMessage("CPF é obrigatório")
            .Must(value => DocumentoUtils.ValidarCpf(value))
            .WithMessage("CPF inválido");
    }
}
