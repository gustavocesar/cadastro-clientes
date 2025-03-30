using CadastroCliente.Application.Commands.Interfaces;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Extensions;
using ErrorOr;

namespace CadastroCliente.Application.Commands;

public class PessoaFisicaCommand : IPessoaFisicaCommand
{
    private readonly IClienteRepository _clienteRepository;

    public PessoaFisicaCommand(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaFisicaCommandResponse>> Criar(PessoaFisicaCommandRequest request)
    {
        var pessoaFisica = (PessoaFisica)request;

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

    public async Task<ErrorOr<PessoaFisicaCommandResponse>> Atualizar(Guid id, PessoaFisicaCommandRequest request)
    {
        var cliente = await _clienteRepository.ObterPorId(id);

        if (cliente is null)
            return Error.NotFound();

        if (await ClienteJaCadastrado(request, id))
            return Error.Validation(description: "Cliente já cadastrado");

        var pessoaFisica = (cliente as PessoaFisica)!;

        pessoaFisica
            .AtualizarNome(request.Nome)
            .AtualizarCpf(request.Cpf)
            .AtualizarDataNascimento(request.DataNascimento)
            .AtualizarTelefone(request.Telefone)
            .AtualizarEmail(request.Email)
            .AtualizarEndereco(
                request.Endereco.Cep,
                request.Endereco.Logradouro,
                request.Endereco.Numero,
                request.Endereco.Bairro,
                request.Endereco.Cidade,
                request.Endereco.Estado
           );

        var validationResult = pessoaFisica.Validate();

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        await _clienteRepository.Atualizar(pessoaFisica);

        return new PessoaFisicaCommandResponse(pessoaFisica);
    }

    public async Task<ErrorOr<PessoaFisicaCommandResponse>> Deletar(Guid id)
    {
        var cliente = await _clienteRepository.Deletar(id);

        if (cliente is null)
            return Error.NotFound();

        return new PessoaFisicaCommandResponse(cliente);
    }
}
