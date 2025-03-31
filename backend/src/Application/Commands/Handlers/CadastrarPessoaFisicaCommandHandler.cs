using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Extensions;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands.Handlers;

public class CadastrarPessoaFisicaCommandHandler : IRequestHandler<CadastrarPessoaFisicaCommand, ErrorOr<PessoaFisicaCommandResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public CadastrarPessoaFisicaCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaFisicaCommandResponse>> Handle(CadastrarPessoaFisicaCommand command, CancellationToken cancellationToken)
    {
        var pessoaFisica = (PessoaFisica)command;

        if (await ClienteJaCadastrado(pessoaFisica))
            return Error.Validation(description: "Cliente já cadastrado");

        var validationResult = pessoaFisica.Validate();

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var cliente = await _clienteRepository.Criar(pessoaFisica);

        return new PessoaFisicaCommandResponse(cliente);
    }

    private async Task<bool> ClienteJaCadastrado(PessoaFisica pessoaFisica, Guid? idUpdated = null)
    {
        return await _clienteRepository.EmailJaCadastrado(pessoaFisica.Email, idUpdated) ||
            await _clienteRepository.CpfJaCadastrado(pessoaFisica.Cpf, idUpdated);
    }
}
