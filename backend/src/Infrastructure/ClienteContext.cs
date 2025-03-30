using CadastroCliente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Infrastructure;

[ExcludeFromCodeCoverage]
public class ClienteContext : DbContext
{
    public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteContext).Assembly);
    }
}
