using CadastroCliente.Application.Query.Responses;
using ErrorOr;

namespace CadastroCliente.Application.Queries.Interfaces;

public interface IPessoaJuridicaQuery
{
    Task<ErrorOr<PessoaJuridicaQueryResponse>> ObterPorId(Guid id);
}

