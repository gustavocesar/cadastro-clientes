using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands;

[ExcludeFromCodeCoverage]
public class AlterarPessoaFisicaCommand : IRequest<ErrorOr<PessoaFisicaCommandResponse>>
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public DateTime DataNascimento { get; set; } = DateTime.Now;
    public string Telefone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public EnderecoCommandRequest Endereco { get; set; } = new();

    public static implicit operator PessoaFisica(AlterarPessoaFisicaCommand command)
    {
        var endereco = new Endereco(
            command.Endereco.Cep,
            command.Endereco.Logradouro,
            command.Endereco.Numero,
            command.Endereco.Bairro,
            command.Endereco.Cidade,
            command.Endereco.Estado
        );

        return new PessoaFisica(
            command.Cpf,
            command.Nome,
            command.Telefone,
            command.Email,
            command.DataNascimento,
            endereco
        );
    }
}


