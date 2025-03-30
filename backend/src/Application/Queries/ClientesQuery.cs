using CadastroCliente.Application.Queries.Interfaces;
using CadastroCliente.Application.Query.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;

namespace CadastroCliente.Application.Queries;

public class ClientesQuery : IClientesQuery
{
    private readonly IClienteRepository _clienteRepository;

    public ClientesQuery(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteQueryResponse>> ObterTodos()
    {
        var clientes = await _clienteRepository.ObterTodos();

        var pessoasFisicas = clientes.OfType<PessoaFisica>()
            .Select(pf => new ClienteQueryResponse(pf));

        var pessoasJuridicas = clientes.OfType<PessoaJuridica>()
            .Select(pj => new ClienteQueryResponse(pj));

        return pessoasFisicas.Union(pessoasJuridicas);
    }
}
