using CadastroCliente.Application.Query.Responses;

namespace CadastroCliente.Application.Queries.Interfaces;

public interface IClientesQuery
{
    Task<IEnumerable<ClienteQueryResponse>> ObterTodos();
}
