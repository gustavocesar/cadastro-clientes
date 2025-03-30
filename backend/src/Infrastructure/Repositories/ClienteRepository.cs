using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public class ClienteRepository : IClienteRepository
{
    private readonly ClienteContext _context;

    public ClienteRepository(ClienteContext context)
    {
        _context = context;
    }

    public async Task<Cliente> ObterPorId(Guid id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Endereco)
            .FirstOrDefaultAsync(c => c.Id == id)!;

        return cliente!;
    }

    public async Task<Cliente> ObterPorIdAsNoTracking(Guid id)
    {
        var cliente = await _context.Clientes
            .AsNoTracking()
            .Include(c => c.Endereco)
            .FirstOrDefaultAsync(c => c.Id == id);

        return cliente!;
    }

    public async Task<IEnumerable<Cliente>> ObterTodos()
    {
        return await _context.Clientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente> Criar(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }

    public async Task<Cliente> Atualizar(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }

    public async Task<Cliente> Deletar(Guid id)
    {
        var cliente = await ObterPorId(id);

        if (cliente is null)
            return null!;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }

    public async Task<bool> EmailJaCadastrado(string email, Guid? idUpdated = null) =>
        await _context.Clientes
            .AsNoTracking()
            .AnyAsync(cliente => cliente.Email == email && cliente.Id != idUpdated);

    public async Task<bool> CpfJaCadastrado(string cpf, Guid? idUpdated = null) =>
        await _context.Clientes
            .AsNoTracking()
            .AnyAsync(cliente => ((PessoaFisica)cliente).Cpf == cpf && cliente.Id != idUpdated);

    public async Task<bool> CnpjJaCadastrado(string cnpj, Guid? idUpdated = null) =>
        await _context.Clientes
            .AsNoTracking()
            .AnyAsync(cliente => ((PessoaJuridica)cliente).Cnpj == cnpj && cliente.Id != idUpdated);
}

