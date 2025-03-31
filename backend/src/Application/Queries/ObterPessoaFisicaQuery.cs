using System.Diagnostics.CodeAnalysis;
using CadastroCliente.Application.Query.Responses;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Queries;

[ExcludeFromCodeCoverage]
public class ObterPessoaFisicaQuery : IRequest<ErrorOr<PessoaFisicaQueryResponse>>
{
    public Guid Id { get; set; }
}
