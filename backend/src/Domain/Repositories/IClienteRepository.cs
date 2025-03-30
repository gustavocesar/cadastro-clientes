using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Domain.Repositories;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodos();
    Task<Cliente> ObterPorId(Guid id);
    Task<Cliente> ObterPorIdAsNoTracking(Guid id);
    Task<Cliente> Criar(Cliente cliente);
    Task<Cliente> Atualizar(Cliente cliente);
    Task<Cliente> Deletar(Guid id);
    Task<bool> EmailJaCadastrado(string email, Guid? idUpdated = null);
    Task<bool> CpfJaCadastrado(string cpf, Guid? idUpdated = null);
    Task<bool> CnpjJaCadastrado(string cnpj, Guid? idUpdated = null);
}

