using CadastroCliente.Domain.Entities.Base;
using CadastroCliente.Domain.Validators;
using CadastroCliente.Extensions;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Domain.Entities;

public class Endereco : Entity
{
    [ExcludeFromCodeCoverage]
    protected Endereco() { }

    public Endereco(string? cep, string? logradouro, string? numero, string? bairro, string? cidade, string? estado)
    {
        CEP = cep.RemoverCaracteresNaoNumericos();
        Logradouro = logradouro?.Trim();
        Numero = numero?.Trim();
        Bairro = bairro?.Trim();
        Cidade = cidade?.Trim();
        Estado = estado?.Trim();

        Validate();
    }

    public string? CEP { get; private set; }
    public string? Logradouro { get; private set; }
    public string? Numero { get; private set; }
    public string? Bairro { get; private set; }
    public string? Estado { get; private set; }
    public string? Cidade { get; private set; }
    public Guid ClienteId { get; private set; }

    public override ValidationResult Validate()
    {
        var validator = new EnderecoValidator();

        return validator.Validate(this);
    }

    public Endereco AtualizarCep(string? cep)
    {
        CEP = cep.RemoverCaracteresNaoNumericos();

        Validate();

        return this;
    }

    public Endereco AtualizarLogradouro(string? logradouro)
    {
        Logradouro = logradouro;

        Validate();

        return this;
    }

    public Endereco AtualizarNumero(string? numero)
    {
        Numero = numero;

        Validate();

        return this;
    }

    public Endereco AtualizarBairro(string? bairro)
    {
        Bairro = bairro;

        Validate();

        return this;
    }

    public Endereco AtualizarCidade(string? cidade)
    {
        Cidade = cidade;

        Validate();

        return this;
    }

    public Endereco AtualizarEstado(string? estado)
    {
        Estado = estado;

        Validate();

        return this;
    }
}
