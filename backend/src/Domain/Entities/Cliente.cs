using CadastroCliente.Domain.Entities.Base;
using CadastroCliente.Domain.Validators;
using CadastroCliente.Extensions;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Domain.Entities;

public abstract class Cliente : Entity
{
    [ExcludeFromCodeCoverage]
    protected Cliente() { }

    protected Cliente(string tipo, string telefone, string email, DateTime dataNascimento, Endereco endereco)
    {
        Telefone = telefone.RemoverCaracteresNaoNumericos();
        Email = email;
        DataNascimento = dataNascimento;
        Tipo = tipo;
        Endereco = endereco;

        Validate();
    }

    public string Telefone { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime DataNascimento { get; private set; } = DateTime.Now;
    public string Tipo { get; private set; } = null!;
    public Endereco Endereco { get; private set; } = null!;

    public override ValidationResult Validate()
    {
        var validator = new ClienteValidator();
        return validator.Validate(this);
    }

    public Cliente AtualizarDataNascimento(DateTime dataNascimento)
    {
        DataNascimento = dataNascimento;

        Validate();

        return this;
    }

    public Cliente AtualizarTelefone(string telefone)
    {
        Telefone = telefone;

        Validate();

        return this;
    }

    public Cliente AtualizarEmail(string email)
    {
        Email = email;

        Validate();

        return this;
    }

    public Cliente AtualizarEndereco(string? cep, string? logradouro, string? numero, string? bairro, string? cidade, string? estado)
    {
        Endereco?.AtualizarCep(cep)
            .AtualizarLogradouro(logradouro)
            .AtualizarNumero(numero)
            .AtualizarBairro(bairro)
            .AtualizarCidade(cidade)
            .AtualizarEstado(estado);

        Validate();

        return this;
    }
}