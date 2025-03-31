using Application.Queries;
using CadastroCliente.Application.Query.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using MediatR;

namespace CadastroCliente.Application.Queries.Handlers;

public class ObterTodosClientesQueryHandler : IRequestHandler<ObterTodosClientesQuery, IEnumerable<ClienteQueryResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public ObterTodosClientesQueryHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteQueryResponse>> Handle(ObterTodosClientesQuery request, CancellationToken cancellationToken)
    {
        var clientes = await _clienteRepository.ObterTodos();

        var pessoasFisicas = clientes.OfType<PessoaFisica>()
            .Select(pf => new ClienteQueryResponse(pf));

        var pessoasJuridicas = clientes.OfType<PessoaJuridica>()
            .Select(pj => new ClienteQueryResponse(pj));

        return pessoasFisicas.Union(pessoasJuridicas);
    }
}
