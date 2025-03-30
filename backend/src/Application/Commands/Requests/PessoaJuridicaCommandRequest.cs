using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Application.Commands.Requests;

[ExcludeFromCodeCoverage]
public class PessoaJuridicaCommandRequest
{
    public string RazaoSocial { get; set; } = null!;
    public string Cnpj { get; set; } = null!;
    public string? InscricaoEstadual { get; set; }
    public bool Isento { get; set; }
    public string Telefone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime DataNascimento { get; set; } = DateTime.Now;
    public EnderecoCommandRequest Endereco { get; set; } = new();


    public static implicit operator PessoaJuridica(PessoaJuridicaCommandRequest request)
    {
        var endereco = new Endereco(
            request.Endereco.Cep,
            request.Endereco.Logradouro,
            request.Endereco.Numero,
            request.Endereco.Bairro,
            request.Endereco.Cidade,
            request.Endereco.Estado
        );

        return new PessoaJuridica(
            request.Cnpj,
            request.RazaoSocial,
            request.Telefone,
            request.Email,
            request.DataNascimento,
            request.InscricaoEstadual,
            request.Isento,
            endereco
        );
    }
}
