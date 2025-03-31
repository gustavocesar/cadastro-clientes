using CadastroCliente.Application.Query.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Queries.Handlers;

public class ObterPessoaFisicaQueryHandler : IRequestHandler<ObterPessoaFisicaQuery, ErrorOr<PessoaFisicaQueryResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public ObterPessoaFisicaQueryHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaFisicaQueryResponse>> Handle(ObterPessoaFisicaQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.ObterPorIdAsNoTracking(request.Id);

        if (cliente is null)
            return Error.NotFound();

        return new PessoaFisicaQueryResponse((cliente as PessoaFisica)!);
    }
}
