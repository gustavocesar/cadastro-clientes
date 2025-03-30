using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Application.Commands.Responses;

[ExcludeFromCodeCoverage]
public class PessoaFisicaCommandResponse
{
    public PessoaFisicaCommandResponse(Cliente cliente)
    {
        Id = cliente.Id;
        Telefone = cliente.Telefone;
        Email = cliente.Email;
        DataNascimento = cliente.DataNascimento;

        var pessoaFisica = (cliente as PessoaFisica)!;

        Nome = pessoaFisica.Nome;
        Documento = pessoaFisica.Cpf;
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
}
