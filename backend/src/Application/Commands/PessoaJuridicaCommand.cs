using CadastroCliente.Application.Commands.Interfaces;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Extensions;
using ErrorOr;

namespace CadastroCliente.Application.Commands;

public class PessoaJuridicaCommand : IPessoaJuridicaCommand
{
    private readonly IClienteRepository _clienteRepository;

    public PessoaJuridicaCommand(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<PessoaJuridicaCommandResponse>> Criar(PessoaJuridicaCommandRequest request)
    {
        var pessoaJuridica = (PessoaJuridica)request;

        if (await ClienteJaCadastrado(pessoaJuridica))
            return Error.Validation(description: "Cliente já cadastrado");

        var validationResult = pessoaJuridica.Validate();

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var cliente = await _clienteRepository.Criar(pessoaJuridica);

        return new PessoaJuridicaCommandResponse(cliente);
    }

    public async Task<ErrorOr<PessoaJuridicaCommandResponse>> Atualizar(Guid id, PessoaJuridicaCommandRequest request)
    {
        var cliente = await _clienteRepository.ObterPorId(id);

        if (cliente is null)
            return Error.NotFound();

        if (await ClienteJaCadastrado(request, id))
            return Error.Validation(description: "Cliente já cadastrado");

        var pessoaJuridica = (cliente as PessoaJuridica)!;

        pessoaJuridica
            .AtualizarRazaoSocial(request.RazaoSocial)
            .AtualizarCnpj(request.Cnpj)
            .AtualizarInscricaoEstadual(request.InscricaoEstadual)
            .AtualizarIsento(request.Isento)
            .AtualizarTelefone(request.Telefone)
            .AtualizarEmail(request.Email)
            .AtualizarDataNascimento(request.DataNascimento)
            .AtualizarEndereco(
                request.Endereco.Cep,
                request.Endereco.Logradouro,
                request.Endereco.Numero,
                request.Endereco.Bairro,
                request.Endereco.Cidade,
                request.Endereco.Estado
           );

        var validationResult = pessoaJuridica.Validate();

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        await _clienteRepository.Atualizar(pessoaJuridica);

        return new PessoaJuridicaCommandResponse(cliente);
    }

    public async Task<ErrorOr<PessoaJuridicaCommandResponse>> Deletar(Guid id)
    {
        var cliente = await _clienteRepository.Deletar(id);

        if (cliente is null)
            return Error.NotFound();

        return new PessoaJuridicaCommandResponse(cliente);
    }

    private async Task<bool> ClienteJaCadastrado(PessoaJuridica pessoaJuridica, Guid? idUpdated = null)
    {
        return await _clienteRepository.EmailJaCadastrado(pessoaJuridica.Email, idUpdated) ||
            await _clienteRepository.CnpjJaCadastrado(pessoaJuridica.Cnpj, idUpdated);
    }
}
