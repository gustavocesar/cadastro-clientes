using CadastroCliente.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Application.Commands.Responses;

[ExcludeFromCodeCoverage]
public class PessoaJuridicaCommandResponse
{
    public PessoaJuridicaCommandResponse(Cliente cliente)
    {
        Id = cliente.Id;
        Telefone = cliente.Telefone;
        Email = cliente.Email;
        DataNascimento = cliente.DataNascimento;

        var pessoaJuridica = (cliente as PessoaJuridica)!;

        Nome = pessoaJuridica.RazaoSocial;
        Documento = pessoaJuridica.Cnpj;
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
}

