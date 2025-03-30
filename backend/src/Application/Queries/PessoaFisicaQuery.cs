using CadastroCliente.Application.Queries.Interfaces;
using CadastroCliente.Application.Query.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using ErrorOr;

namespace CadastroCliente.Application.Queries;

public class PessoaFisicaQuery : IPessoaFisicaQuery
{
    private readonly IClienteRepository _clienteRepository;

    public PessoaFisicaQuery(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaFisicaQueryResponse>> ObterPorId(Guid id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsNoTracking(id);

        if (cliente is null)
            return Error.NotFound();

        return new PessoaFisicaQueryResponse((cliente as PessoaFisica)!);
    }
}

