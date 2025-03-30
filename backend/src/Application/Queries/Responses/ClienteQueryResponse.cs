using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Application.Query.Responses;

[ExcludeFromCodeCoverage]
public class ClienteQueryResponse
{
    public ClienteQueryResponse(PessoaFisica pf)
    {
        Id = pf.Id;
        Nome = pf.Nome;
        Documento = pf.Cpf;
        Telefone = pf.Telefone;
        Email = pf.Email;
        DataNascimento = pf.DataNascimento;
        Tipo = pf.Tipo;
        Endereco = new EnderecoQueryResponse
        {
            CEP = pf.Endereco?.CEP,
            Logradouro = pf.Endereco?.Logradouro,
            Numero = pf.Endereco?.Numero,
            Bairro = pf.Endereco?.Bairro,
            Cidade = pf.Endereco?.Cidade,
            Estado = pf.Endereco?.Estado
        };
    }

    public ClienteQueryResponse(PessoaJuridica pj)
    {
        Id = pj.Id;
        Nome = pj.RazaoSocial;
        Documento = pj.Cnpj;
        Telefone = pj.Telefone;
        Email = pj.Email;
        DataNascimento = pj.DataNascimento;
        Tipo = pj.Tipo;
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
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Tipo { get; set; }
    public EnderecoQueryResponse Endereco { get; set; }
}
