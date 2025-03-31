using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands.Handlers;

public class DeletarPessoaJuridicaCommandHandler : IRequestHandler<DeletarPessoaJuridicaCommand, ErrorOr<PessoaJuridicaCommandResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public DeletarPessoaJuridicaCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaJuridicaCommandResponse>> Handle(DeletarPessoaJuridicaCommand command, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.Deletar(command.Id);

        if (cliente is null)
            return Error.NotFound();

        return new PessoaJuridicaCommandResponse(cliente);
    }
}
