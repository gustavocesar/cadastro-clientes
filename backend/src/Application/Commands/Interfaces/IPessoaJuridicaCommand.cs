using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands.Responses;
using CadastroCliente.Domain.Entities;
using ErrorOr;

namespace CadastroCliente.Application.Commands.Interfaces;

public interface IPessoaJuridicaCommand
{
    Task<ErrorOr<PessoaJuridicaCommandResponse>> Criar(PessoaJuridicaCommandRequest request);
    Task<ErrorOr<PessoaJuridicaCommandResponse>> Atualizar(Guid id, PessoaJuridicaCommandRequest request);
    Task<ErrorOr<PessoaJuridicaCommandResponse>> Deletar(Guid id);
}

