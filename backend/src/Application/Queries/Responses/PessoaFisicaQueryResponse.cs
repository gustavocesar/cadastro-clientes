using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Application.Query.Responses;

[ExcludeFromCodeCoverage]
public class PessoaFisicaQueryResponse
{
    public PessoaFisicaQueryResponse(PessoaFisica pf)
    {
        Id = pf.Id;
        Nome = pf.Nome;
        Cpf = pf.Cpf;
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

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Tipo { get; set; }
    public EnderecoQueryResponse Endereco { get; set; }
}
