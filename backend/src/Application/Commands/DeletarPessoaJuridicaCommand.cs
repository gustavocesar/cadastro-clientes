using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Application.Commands.Responses;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands;

[ExcludeFromCodeCoverage]
public class DeletarPessoaJuridicaCommand : IRequest<ErrorOr<PessoaJuridicaCommandResponse>>
{
    public Guid Id { get; set; }
}
