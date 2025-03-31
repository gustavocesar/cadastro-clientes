using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Application.Query.Responses;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Queries;

[ExcludeFromCodeCoverage]
public class ObterPessoaJuridicaQuery : IRequest<ErrorOr<PessoaJuridicaQueryResponse>>
{
    public Guid Id { get; set; }
}
