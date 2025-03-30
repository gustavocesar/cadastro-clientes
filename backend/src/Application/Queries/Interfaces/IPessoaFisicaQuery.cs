using CadastroCliente.Application.Query.Responses;
using ErrorOr;

namespace CadastroCliente.Application.Queries.Interfaces;

public interface IPessoaFisicaQuery
{
    Task<ErrorOr<PessoaFisicaQueryResponse>> ObterPorId(Guid id);
}
