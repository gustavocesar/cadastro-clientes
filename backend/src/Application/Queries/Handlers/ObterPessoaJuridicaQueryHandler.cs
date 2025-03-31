using CadastroCliente.Application.Query.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Queries.Handlers;

public class ObterPessoaJuridicaQueryHandler : IRequestHandler<ObterPessoaJuridicaQuery, ErrorOr<PessoaJuridicaQueryResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public ObterPessoaJuridicaQueryHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaJuridicaQueryResponse>> Handle(ObterPessoaJuridicaQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.ObterPorIdAsNoTracking(request.Id);

        if (cliente is null)
            return Error.NotFound();

        return new PessoaJuridicaQueryResponse((cliente as PessoaJuridica)!);
    }
}
