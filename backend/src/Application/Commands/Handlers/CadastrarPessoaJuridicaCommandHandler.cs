using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Extensions;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands.Handlers;

public class CadastrarPessoaJuridicaCommandHandler : IRequestHandler<CadastrarPessoaJuridicaCommand, ErrorOr<PessoaJuridicaCommandResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public CadastrarPessoaJuridicaCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaJuridicaCommandResponse>> Handle(CadastrarPessoaJuridicaCommand command, CancellationToken cancellationToken)
    {
        var pessoaJuridica = (PessoaJuridica)command;

        if (await ClienteJaCadastrado(pessoaJuridica))
            return Error.Validation(description: "Cliente já cadastrado");

        var validationResult = pessoaJuridica.Validate();

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var cliente = await _clienteRepository.Criar(pessoaJuridica);

        return new PessoaJuridicaCommandResponse(cliente);
    }

    private async Task<bool> ClienteJaCadastrado(PessoaJuridica pessoaJuridica, Guid? idUpdated = null)
    {
        return await _clienteRepository.EmailJaCadastrado(pessoaJuridica.Email, idUpdated) ||
            await _clienteRepository.CnpjJaCadastrado(pessoaJuridica.Cnpj, idUpdated);
    }
}
