using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Application.Commands.Responses;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands;

[ExcludeFromCodeCoverage]
public class DeletarPessoaFisicaCommand : IRequest<ErrorOr<PessoaFisicaCommandResponse>>
{
    public Guid Id { get; set; }
}
