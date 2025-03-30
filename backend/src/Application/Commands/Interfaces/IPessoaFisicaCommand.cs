using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands.Responses;
using ErrorOr;

namespace CadastroCliente.Application.Commands.Interfaces;

public interface IPessoaFisicaCommand
{
    Task<ErrorOr<PessoaFisicaCommandResponse>> Criar(PessoaFisicaCommandRequest request);
    Task<ErrorOr<PessoaFisicaCommandResponse>> Atualizar(Guid id, PessoaFisicaCommandRequest request);
    Task<ErrorOr<PessoaFisicaCommandResponse>> Deletar(Guid id);
}

