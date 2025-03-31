using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands;

[ExcludeFromCodeCoverage]
public class CadastrarPessoaJuridicaCommand : IRequest<ErrorOr<PessoaJuridicaCommandResponse>>
{
    public string RazaoSocial { get; set; } = null!;
    public string Cnpj { get; set; } = null!;
    public string? InscricaoEstadual { get; set; }
    public bool Isento { get; set; }
    public string Telefone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime DataNascimento { get; set; } = DateTime.Now;
    public EnderecoCommandRequest Endereco { get; set; } = new();

    public static implicit operator PessoaJuridica(CadastrarPessoaJuridicaCommand request)
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
