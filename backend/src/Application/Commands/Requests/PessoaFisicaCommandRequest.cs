using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Application.Commands.Requests;

[ExcludeFromCodeCoverage]
public class PessoaFisicaCommandRequest
{
    public string Nome { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public DateTime DataNascimento { get; set; } = DateTime.Now;
    public string Telefone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public EnderecoCommandRequest Endereco { get; set; } = new();

    public static implicit operator PessoaFisica(PessoaFisicaCommandRequest request)
    {
        var endereco = new Endereco(
            request.Endereco.Cep,
            request.Endereco.Logradouro,
            request.Endereco.Numero,
            request.Endereco.Bairro,
            request.Endereco.Cidade,
            request.Endereco.Estado
        );

        return new PessoaFisica(
            request.Cpf,
            request.Nome,
            request.Telefone,
            request.Email,
            request.DataNascimento,
            endereco
        );
    }
}
