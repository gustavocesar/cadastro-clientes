using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Extensions;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands.Handlers;

public class AlterarPessoaFisicaCommandHandler : IRequestHandler<AlterarPessoaFisicaCommand, ErrorOr<PessoaFisicaCommandResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public AlterarPessoaFisicaCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaFisicaCommandResponse>> Handle(AlterarPessoaFisicaCommand command, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.ObterPorId(command.Id);

        if (cliente is null)
            return Error.NotFound();

        if (await ClienteJaCadastrado(command, command.Id))
            return Error.Validation(description: "Cliente já cadastrado");

        var pessoaFisica = (cliente as PessoaFisica)!;

        pessoaFisica
            .AtualizarNome(command.Nome)
            .AtualizarCpf(command.Cpf)
            .AtualizarDataNascimento(command.DataNascimento)
            .AtualizarTelefone(command.Telefone)
            .AtualizarEmail(command.Email)
            .AtualizarEndereco(
                command.Endereco.Cep,
                command.Endereco.Logradouro,
                command.Endereco.Numero,
                command.Endereco.Bairro,
                command.Endereco.Cidade,
                command.Endereco.Estado
           );

        var validationResult = pessoaFisica.Validate();

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        await _clienteRepository.Atualizar(pessoaFisica);

        return new PessoaFisicaCommandResponse(pessoaFisica);
    }
    
    private async Task<bool> ClienteJaCadastrado(PessoaFisica pessoaFisica, Guid? idUpdated = null)
    {
        return await _clienteRepository.EmailJaCadastrado(pessoaFisica.Email, idUpdated) ||
            await _clienteRepository.CpfJaCadastrado(pessoaFisica.Cpf, idUpdated);
    }
}   
