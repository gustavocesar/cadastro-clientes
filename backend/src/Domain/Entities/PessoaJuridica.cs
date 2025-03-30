using CadastroCliente.Domain.Enums;
using CadastroCliente.Domain.Validators;
using CadastroCliente.Extensions;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Domain.Entities;

public class PessoaJuridica : Cliente
{
    [ExcludeFromCodeCoverage]
    protected PessoaJuridica() { }

    public PessoaJuridica(
        string cnpj,
        string razaoSocial,
        string telefone,
        string email,
        DateTime dataNascimento,
        string? inscricaoEstadual,
        bool isento,
        Endereco endereco
    ) : base(nameof(TipoCliente.PessoaJuridica), telefone, email, dataNascimento, endereco)
    {
        Cnpj = cnpj.RemoverCaracteresNaoNumericos();
        RazaoSocial = razaoSocial;
        InscricaoEstadual = inscricaoEstadual;
        Isento = isento;

        Validate();
    }

    public string Cnpj { get; private set; } = null!;
    public string RazaoSocial { get; private set; } = null!;
    public string? InscricaoEstadual { get; private set; } = null!;
    public bool Isento { get; private set; }

    public override ValidationResult Validate()
    {
        var validator = new PessoaJuridicaValidator();
        var pjValidations = validator.Validate(this);

        var baseValidations = base.Validate();
        pjValidations.Errors.AddRange(baseValidations.Errors);

        return pjValidations;
    }

    public PessoaJuridica AtualizarRazaoSocial(string razaoSocial)
    {
        RazaoSocial = razaoSocial;

        Validate();

        return this;
    }

    public PessoaJuridica AtualizarCnpj(string cnpj)
    {
        Cnpj = cnpj.RemoverCaracteresNaoNumericos();

        Validate();

        return this;
    }

    public PessoaJuridica AtualizarInscricaoEstadual(string? inscricaoEstadual)
    {
        InscricaoEstadual = inscricaoEstadual;

        Validate();

        return this;
    }

    public PessoaJuridica AtualizarIsento(bool isento)
    {
        Isento = isento;

        if (isento)
            InscricaoEstadual = null;

        Validate();

        return this;
    }
}