using CadastroCliente.Application.Query.Responses;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Queries;

[ExcludeFromCodeCoverage]
public class ObterTodosClientesQuery : IRequest<IEnumerable<ClienteQueryResponse>>
{
}

