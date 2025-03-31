using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands.Handlers;

public class DeletarPessoaFisicaCommandHandler : IRequestHandler<DeletarPessoaFisicaCommand, ErrorOr<PessoaFisicaCommandResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public DeletarPessoaFisicaCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaFisicaCommandResponse>> Handle(DeletarPessoaFisicaCommand command, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.Deletar(command.Id);

        if (cliente is null)
            return Error.NotFound();

        return new PessoaFisicaCommandResponse(cliente);
    }
}
