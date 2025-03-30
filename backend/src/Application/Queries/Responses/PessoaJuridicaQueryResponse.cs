using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Application.Query.Responses;

[ExcludeFromCodeCoverage]
public class PessoaJuridicaQueryResponse
{
    public PessoaJuridicaQueryResponse(PessoaJuridica pj)
    {
        Id = pj.Id;
        RazaoSocial = pj.RazaoSocial;
        Cnpj = pj.Cnpj;
        Telefone = pj.Telefone;
        Email = pj.Email;
        DataNascimento = pj.DataNascimento;
        Tipo = pj.Tipo;
        InscricaoEstadual = pj.InscricaoEstadual;
        Isento = pj.Isento;
        Endereco = new EnderecoQueryResponse
        {
            CEP = pj.Endereco?.CEP,
            Logradouro = pj.Endereco?.Logradouro,
            Numero = pj.Endereco?.Numero,
            Bairro = pj.Endereco?.Bairro,
            Cidade = pj.Endereco?.Cidade,
            Estado = pj.Endereco?.Estado
        };
    }

    public Guid Id { get; set; }
    public string RazaoSocial { get; set; }
    public string Cnpj { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Tipo { get; set; }
    public string? InscricaoEstadual { get; set; }
    public bool Isento { get; set; }
    public EnderecoQueryResponse Endereco { get; set; }
}

