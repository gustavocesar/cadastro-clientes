using CadastroCliente.Domain.Enums;
using CadastroCliente.Domain.Validators;
using CadastroCliente.Extensions;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Domain.Entities;

public class PessoaFisica : Cliente
{
    [ExcludeFromCodeCoverage]
    protected PessoaFisica() { }

    public PessoaFisica(
        string cpf,
        string nome,
        string telefone,
        string email,
        DateTime dataNascimento,
        Endereco endereco
    ) : base(nameof(TipoCliente.PessoaFisica), telefone, email, dataNascimento, endereco)
    {
        Cpf = cpf.RemoverCaracteresNaoNumericos();
        Nome = nome;

        Validate();
    }

    public string Cpf { get; private set; } = null!;
    public string Nome { get; private set; } = null!;

    public override ValidationResult Validate()
    {
        var validator = new PessoaFisicaValidator();
        var pfValidations = validator.Validate(this);

        var baseValidations = base.Validate();
        pfValidations.Errors.AddRange(baseValidations.Errors);

        if (IdadeMenorQue18Anos())
            pfValidations.Errors.Add(new ValidationFailure(nameof(DataNascimento), "O cliente deve ter pelo menos 18 anos"));

        return pfValidations;
    }

    private bool IdadeMenorQue18Anos() =>
         DateTime.Now.Year - DataNascimento.Year < 18;

    public PessoaFisica AtualizarNome(string nome)
    {
        Nome = nome;

        Validate();

        return this;
    }

    public PessoaFisica AtualizarCpf(string cpf)
    {
        Cpf = cpf.RemoverCaracteresNaoNumericos();

        Validate();

        return this;
    }
}