using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Extensions;
using ErrorOr;
using MediatR;

namespace CadastroCliente.Application.Commands.Handlers;

public class AlterarPessoaJuridicaCommandHandler : IRequestHandler<AlterarPessoaJuridicaCommand, ErrorOr<PessoaJuridicaCommandResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public AlterarPessoaJuridicaCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaJuridicaCommandResponse>> Handle(AlterarPessoaJuridicaCommand command, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.ObterPorId(command.Id);

        if (cliente is null)
            return Error.NotFound();

        if (await ClienteJaCadastrado(command, command.Id))
            return Error.Validation(description: "Cliente já cadastrado");

        var pessoaJuridica = (cliente as PessoaJuridica)!;

        pessoaJuridica
            .AtualizarRazaoSocial(command.RazaoSocial)
            .AtualizarCnpj(command.Cnpj)
            .AtualizarInscricaoEstadual(command.InscricaoEstadual)
            .AtualizarIsento(command.Isento)
            .AtualizarTelefone(command.Telefone)
            .AtualizarEmail(command.Email)
            .AtualizarDataNascimento(command.DataNascimento)
            .AtualizarEndereco(
                command.Endereco.Cep,
                command.Endereco.Logradouro,
                command.Endereco.Numero,
                command.Endereco.Bairro,
                command.Endereco.Cidade,
                command.Endereco.Estado
           );

        var validationResult = pessoaJuridica.Validate();

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        await _clienteRepository.Atualizar(pessoaJuridica);

        return new PessoaJuridicaCommandResponse(cliente);
    }

    private async Task<bool> ClienteJaCadastrado(PessoaJuridica pessoaJuridica, Guid? idUpdated = null)
    {
        return await _clienteRepository.EmailJaCadastrado(pessoaJuridica.Email, idUpdated) ||
            await _clienteRepository.CnpjJaCadastrado(pessoaJuridica.Cnpj, idUpdated);
    }
}
